import { OnDestroy, Directive } from "@angular/core";
import { Subscription } from "rxjs";

// Workaround to fix 'Class is using Angular features but is not decorated. Please add an explicit Angular decorator'
// https://stackoverflow.com/a/60116400
@Directive()
export class BaseNgDestroy implements OnDestroy {
    protected subs: Subscription[] = [];

    public ngOnDestroy(): void {
        if (this.subs && this.subs.length > 0) {
            this.subs.forEach((x: Subscription) => x && x.unsubscribe());
        }
    }
}
