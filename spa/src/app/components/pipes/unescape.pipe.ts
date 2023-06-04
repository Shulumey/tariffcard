import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: "unescape"
})
export class UnescapePipe implements PipeTransform {
    public transform(value: string): string {
        const doc: Document = new DOMParser().parseFromString(value, "text/html");
        return doc.documentElement.textContent;
    }
}
