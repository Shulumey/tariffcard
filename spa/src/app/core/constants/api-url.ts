/* eslint-disable @typescript-eslint/naming-convention */
import {ObjectType, SellerType} from "../enums";
import { environment } from "src/environments/environment";

const BASE: string = environment.apiUrl;

export const API_URL: {
    GET_OBJECT_GROUPS: (houseGroupId: number) => string;
    GET_HOUSE_GROUPS: (complexId: number) => string;
    GET_COMPLEXES: (regionGroupId: number, objectType: ObjectType, sellerType: SellerType) => string;
} = {
    GET_COMPLEXES: (regionGroupId: number, objectType: ObjectType, sellerType: SellerType): string =>
        `${BASE}/${regionGroupId}/Complexes/type/${objectType}/${sellerType}`,
    GET_HOUSE_GROUPS: (complexId: number): string =>
        `${BASE}/Complexes/${complexId}/house-groups`,
    GET_OBJECT_GROUPS: (houseGroupId: number): string =>
        `${BASE}/HouseGroups/${houseGroupId}/object-groups`
};
