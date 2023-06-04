import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit } from "@angular/core";
import { BaseNgDestroy, COMMISSION_TYPE_DICT, Complex, HouseGroup, ObjectGroup } from "../../core";
import { CommissionType } from "../../core";
import { Extensions } from "../../extend";
import { TableRow } from "./models/tableRow";
import { CommissionsService } from "../../core";
import { combineLatest, Observable, of, Subscriber } from "rxjs";
import { AuthService } from "../../core/services/auth.service";
import { UntilDestroy, untilDestroyed } from "@ngneat/until-destroy";
import { NmUserModel } from "nm-widgets";
import { BrokerageRegionService } from "../../core/services/brokerage-region.service";
import { filter, take } from "rxjs/operators";
import { CommissionFormatter } from "../../core/formatters/commission.formatter";

@UntilDestroy()
@Component({
    selector: "tc-commission-table",
    templateUrl: "./commissions-table.component.html",
    styleUrls: ["./commissions-table.component.scss"],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class CommissionsTableComponent extends BaseNgDestroy implements OnInit {
    @Input()
    public set complexes(value: Complex[]) {
        this.initializeDataSource(value);
    }

    public dataSource: TableRow[] = [];

    public displayedColumns: string[];

    private _defaultColumns = ["seller", "complex", "commission"];

    constructor(private cd: ChangeDetectorRef,
                private commissionsService: CommissionsService,
                private authService: AuthService,
                private brokerageRegionService: BrokerageRegionService) {
        super();
    }

    public ngOnInit(): void {
        this.initializeDisplayedColumns();
    }

    public getTypeString(type: CommissionType): string {
        return COMMISSION_TYPE_DICT[type];
    }

    public formatCommission(value?: number, type?: CommissionType): string {
        return CommissionFormatter.format(value, type);
    }

    public expandRow(row: TableRow): void {
        if (row.childRows && row.childRows.length) {
            this.expandChildRows(row);
            this.cd.detectChanges();
            return;
        }
        this.subs = [
            this.loadChildRows(row).subscribe(() => {
                this.expandChildRows(row);
                this.cd.detectChanges();
            })
        ];
    }

    public collapseRow(row: TableRow): void {
        this.collapseExpandedRowsRecursive(row);

        this.cd.detectChanges();
    }

    private initializeDisplayedColumns(): void {
        combineLatest([
            this.authService.user$,
            this.brokerageRegionService.selectedRegionGroupId$
        ]).pipe(
            untilDestroyed(this),
            filter((args: [NmUserModel, number]) => !!args[0] && !!args[1]),
            take(1)
        ).subscribe((args: [NmUserModel, number]) => {
            this.displayedColumns = [...this._defaultColumns];
            if (args[0].regionGroupId !== args[1]) {
                this.displayedColumns = [...this.displayedColumns, "crossRegionCommission"];
            }
            this.cd.detectChanges();
        });
    }

    private initializeDataSource(complexes: Complex[]): void {
        let groupIndex: number = -1;
        let elementIndex: number;
        let lastRow: TableRow = null;
        let groupRow: TableRow = null;
        let sellerName: string;

        this.dataSource = complexes
            .sort((a: Complex, b: Complex) => {
                const res: number = Extensions.sortBy((x: Complex) => x.sellerName)(a, b);
                if (res !== 0) {
                    return res;
                }
                return Extensions.sortBy((x: Complex) => x.complexName)(a, b);
            })
            .reduce((acc: any[], x: Complex) => {
                const row: TableRow = {} as TableRow;
                if (x.sellerName !== sellerName) {
                    sellerName = x.sellerName;
                    groupIndex++;
                    elementIndex = -1;
                    groupRow = row;
                    if (lastRow) {
                        lastRow.last = true;
                    }
                } else {
                    groupRow.rowspan++;
                }

                elementIndex++;

                acc.push(Object.assign(row, {
                    id: x.id,
                    index: elementIndex,
                    type: "complex",
                    groupIndex,
                    groupName: x.sellerName,
                    name: x.complexName,
                    minCommissionValue: x.minCommissionValue,
                    maxCommissionValue: x.maxCommissionValue,
                    commissionType: x.commissionType,
                    crossRegionAdvancedBookingCoefficient: x.crossRegionAdvancedBookingCoefficient,
                    urlLandingPrepaymentBooking: x.urlLandingPrepaymentBooking,
                    expandable: x.housesCount > 0,
                    expanded: false,
                    first: elementIndex === 0,
                    last: false,
                    rowspan: 1,
                    childRows: [],
                    properties: {
                        isSellerCommissionPrepayments: x.isSellerCommissionPrepayments,
                        conditionsOfPaymentFees: x.conditionsOfPaymentFees
                    }
                } as TableRow));

                lastRow = row;

                return acc;
            }, []);
    }

    private loadChildRows(row: TableRow): Observable<void> {
        switch (row.type) {
            case "complex":
                return new Observable<void>((subscriber: Subscriber<void>) => {
                    this.subs = [
                        this.commissionsService.getHouseGroups(row.id).subscribe((houseGroups: HouseGroup[]) => {
                            const houseGroupRows: TableRow[] = houseGroups.map((x: HouseGroup) => ({
                                id: x.id,
                                groupIndex: row.groupIndex,
                                name: x.houseName,
                                maxCommissionValue: x.commissionValue,
                                commissionType: x.commissionType,
                                crossRegionAdvancedBookingCoefficient: x.crossRegionAdvancedBookingCoefficient,
                                expandable: false,
                                type: "house"
                            } as TableRow));
                            row.childRows = houseGroupRows;

                            for (const houseGroupRow of houseGroupRows) {
                                let lastRow: TableRow = houseGroupRow;
                                const houseGroup: HouseGroup = houseGroups.find((x: HouseGroup) => x.id === houseGroupRow.id);
                                const objectGroups: ObjectGroup[] = houseGroup.objectGroups.filter((x: ObjectGroup) => !x.isOverriding);
                                if (objectGroups.length) {
                                    const objectGroupRows: TableRow[] = objectGroups.map((x: ObjectGroup) => ({
                                        id: x.id,
                                        groupIndex: row.groupIndex,
                                        name: x.apartmentDescription,
                                        maxCommissionValue: x.commissionValue,
                                        commissionType: x.commissionType,
                                        crossRegionAdvancedBookingCoefficient: x.crossRegionAdvancedBookingCoefficient,
                                        expandable: false,
                                        type: "object"
                                    } as TableRow));
                                    houseGroupRow.childRows = objectGroupRows;
                                    lastRow = objectGroupRows[objectGroupRows.length - 1];
                                }
                                if (houseGroup.hasOverriding) {
                                    const overridings: ObjectGroup[] = houseGroup.objectGroups.filter((x: ObjectGroup) => x.isOverriding);
                                    lastRow = {
                                        groupIndex: row.groupIndex,
                                        name: "Исключения",
                                        type: "overriding-master",
                                        expandable: true,
                                        childRows: overridings.map((x: ObjectGroup) => ({
                                            id: x.id,
                                            groupIndex: row.groupIndex,
                                            name: x.apartmentDescription,
                                            maxCommissionValue: x.commissionValue,
                                            commissionType: x.commissionType,
                                            crossRegionAdvancedBookingCoefficient: x.crossRegionAdvancedBookingCoefficient,
                                            expandable: false,
                                            type: "object"
                                        } as TableRow))
                                    } as TableRow;
                                    houseGroupRow.childRows = (houseGroupRow.childRows || []).concat(lastRow);
                                }
                            }
                            subscriber.next();
                        })
                    ];
                });
            default:
                return of();
        }
    }

    private expandChildRows(row: TableRow): void {
        if (!row.childRows || !row.childRows.length) {
            return;
        }

        const index: number = this.dataSource.indexOf(row);
        const temp: TableRow[] = this.dataSource.slice();

        temp.splice(index + 1, 0, ...row.childRows);
        this.dataSource = temp;
        row.expanded = true;

        for (const childRow of row.childRows) {
            if (!childRow.expandable && childRow.childRows && childRow.childRows.length) {
                this.expandChildRows(childRow);
            }
        }

        const groupRows: TableRow[] = this.dataSource.filter((x: TableRow) => x.groupIndex === row.groupIndex);

        const groupRow: TableRow = groupRows[0];
        groupRow.rowspan += row.childRows.length;

        CommissionsTableComponent.refreshLastRow(groupRows);
    }

    private collapseExpandedRowsRecursive(row: TableRow): void {
        const childRowsLength: number = row.childRows && row.expanded ? row.childRows.length : 0;

        if (!childRowsLength) {
            return;
        }

        for (const childRow of row.childRows) {
            this.collapseExpandedRowsRecursive(childRow);
        }

        const index: number = this.dataSource.indexOf(row);

        const temp: TableRow[] = this.dataSource.slice();
        temp.splice(index + 1, childRowsLength);

        this.dataSource = temp;
        row.expanded = false;

        const groupRows: TableRow[] = this.dataSource.filter((x: TableRow) => x.groupIndex === row.groupIndex);

        const groupRow: TableRow = groupRows[0];
        groupRow.rowspan -= childRowsLength;

        CommissionsTableComponent.refreshLastRow(groupRows);
    }

    private static refreshLastRow(rows: TableRow[]): void {
        for (const gr of rows) {
            gr.last = false;
        }
        rows[rows.length - 1].last = true;
    }
}
