import { BaseModel } from "./BaseModels";

export interface Product extends BaseModel {
    name: string;
    referenceCode: string;
    description: string;
    value: number;
}

export interface ProductRequest {
    name: string;
    referenceCode: string;
    description: string;
    value: number;
}

export interface ProductRequestUpdate {
    id:string;
    name: string;
    referenceCode: string;
    description: string;
    value: number;
}