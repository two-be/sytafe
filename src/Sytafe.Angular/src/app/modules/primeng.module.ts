import { NgModule } from "@angular/core"
import { BlockUIModule } from "primeng/blockui"
import { ButtonModule } from "primeng/button"
import { CalendarModule } from "primeng/calendar"
import { CardModule } from "primeng/card"
import { CheckboxModule } from "primeng/checkbox"
import { DialogModule } from "primeng/dialog"
import { InputTextModule } from "primeng/inputtext"
import { MenubarModule } from "primeng/menubar"
import { MultiSelectModule } from "primeng/multiselect"
import { ProgressSpinnerModule } from "primeng/progressspinner"
import { RadioButtonModule } from "primeng/radiobutton"
import { RippleModule } from "primeng/ripple"
import { SliderModule } from "primeng/slider"
import { TableModule } from "primeng/table"

@NgModule({
    exports: [
        BlockUIModule,
        ButtonModule,
        CalendarModule,
        CardModule,
        CheckboxModule,
        DialogModule,
        InputTextModule,
        MenubarModule,
        MultiSelectModule,
        ProgressSpinnerModule,
        RadioButtonModule,
        RippleModule,
        SliderModule,
        TableModule,
    ],
})
export class PrimeNgModule { }
