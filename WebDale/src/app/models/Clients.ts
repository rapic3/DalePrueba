import { BaseModel } from "./BaseModels";

export interface Client extends BaseModel {
    nombres: string;
    apellidos: string;
    numeroIdentificacion: string;
    direccion: string;
    celular: string;
    email: string;     
    nombreCompleto: string;
}

export interface ClientRequest {
    nombres: string;
    apellidos: string;
    numeroIdentificacion: string;
    direccion: string;
    celular: string;
    email: string;     
}

export interface ClientRequestUpdate {
    id:string;
    nombres: string;
    apellidos: string;
    numeroIdentificacion: string;
    direccion: string;
    celular: string;
    email: string;     
}