import { ItemOrderRequest } from "./ItemsOrder";

export interface OrderRequest {
    value: number;
    items: ItemOrderRequest[];
    clientId: string;
    factura: string;
}