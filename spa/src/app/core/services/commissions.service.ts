import { Injectable } from "@angular/core";
import { Complex, HouseGroup } from "../models";
import { Observable, Subscriber } from "rxjs";
import { API_URL } from "../constants";
import { HttpService } from "./http.service";
import { ObjectType, SellerType } from "../enums";

@Injectable({
    providedIn: "root"
})
export class CommissionsService {
    constructor(
        private _httpService: HttpService
    ) { }

    public getComplexes(regionGroupId: number, objectType: ObjectType, sellerType: SellerType): Observable<Complex[]> {
        const url: string = `${API_URL.GET_COMPLEXES(regionGroupId, objectType, sellerType)}`;
        return new Observable((observer: Subscriber<Complex[]>) => {
            this._httpService.get<Complex[]>(url, (x: Complex[]) => {
                observer.next(x);
            });
        });
    }

    public getHouseGroups(complexId: number): Observable<HouseGroup[]> {
        const url: string = `${API_URL.GET_HOUSE_GROUPS(complexId)}`;
        return new Observable((observer: Subscriber<HouseGroup[]>) => {
            this._httpService.get<HouseGroup[]>(url, (x: HouseGroup[]) => {
                observer.next(x);
            });
        });
    }
}
