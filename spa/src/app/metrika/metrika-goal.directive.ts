import { AfterViewInit, Directive, ElementRef, Input, Renderer2 } from "@angular/core";
import { IMetrikaGoalEventOptions } from "./interfaces";
import { MetrikaService } from "./metrika.service";

@Directive({
    selector: "[ymGoal]"
})
export class MetrikaGoalDirective implements AfterViewInit {
    @Input() public trackOn = "click";

    @Input() public ymTarget: string;

    @Input() public callback: () => void;

    @Input() public params: any;

    constructor(
        private ym: MetrikaService,
        private renderer: Renderer2,
        private el: ElementRef
    ) {
    }

    public ngAfterViewInit(): void {
        try {
            this.renderer.listen(this.el.nativeElement, this.trackOn, () => {
                if (!this.ymTarget) {
                    return;
                }

                const goalOptions: IMetrikaGoalEventOptions = {
                    target: this.ymTarget || this.trackOn,
                    options: {
                        callback: this.callback,
                        ...this.params
                    }
                };
                this.ym.reachGoal(goalOptions);
            });
        } catch (err) {
            console.error(err);
        }
    }
}
