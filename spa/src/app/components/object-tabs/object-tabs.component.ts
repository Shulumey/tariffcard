import { ChangeDetectionStrategy, ChangeDetectorRef, Component, EventEmitter, Input, Output } from "@angular/core";
import { ObjectType } from "../../core";

@Component({
    selector: "tc-object-tabs",
    templateUrl: "./object-tabs.component.html",
    styleUrls: ["./object-tabs.component.scss"],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ObjectTabsComponent {

    @Input()
    public active: ObjectType = ObjectType.apartment;

    @Input()
    public availableObjectTabs: ObjectType[] = [];

    @Output()
    public activeChange = new EventEmitter<ObjectType>();

    constructor(private changeDetectorRef: ChangeDetectorRef) {
    }

    public onTabChanged(value: ObjectType): void {
        if (this.isActive(value)) {
            return;
        }

        this.active = value;
        this.activeChange.emit(value);
        this.changeDetectorRef.detectChanges();
    }

    public isActive(value: ObjectType): boolean {
        return value === this.active;
    }

    public isHidden(value: ObjectType): boolean {
        return false;
        //return this.availableObjectTabs.findIndex(tab => value === tab) === -1;
    }
}
