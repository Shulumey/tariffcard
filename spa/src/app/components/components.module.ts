import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { MatTableModule } from "@angular/material/table";
import { SellerCellComponent } from "./seller-cell/seller-cell.component";
import { CommissionsTableComponent } from "./commissions-table/commissions-table.component";;
import { HeaderMessageComponent } from "./header-message/header-message.component";
import { MatExpansionModule } from "@angular/material/expansion";
import { UnescapePipe } from "./pipes/unescape.pipe";
import { PaymentTermsComponent } from "./payment-terms/payment-terms.component";
import { MatDialogModule } from "@angular/material/dialog";
import { TooltipModule } from "./tooltip";
import { ObjectTabsComponent } from "./object-tabs/object-tabs.component";
import { SellerTabsComponent } from "./seller-tabs/seller-tabs.component";


@NgModule({
    declarations: [
        SellerCellComponent,
        CommissionsTableComponent,
        HeaderMessageComponent,
        UnescapePipe,
        PaymentTermsComponent,
        ObjectTabsComponent,
        SellerTabsComponent
    ],
    imports: [
        CommonModule,
        MatTableModule,
        MatExpansionModule,
        MatDialogModule,
        TooltipModule
    ],
    exports: [
        SellerCellComponent,
        CommissionsTableComponent,
        HeaderMessageComponent,
        PaymentTermsComponent,
        ObjectTabsComponent,
        SellerTabsComponent
    ]
})
export class ComponentsModule { }
