import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import { Observable } from 'rxjs/internal/Observable';
import { throwError } from 'rxjs/internal/observable/throwError';
import { environment } from 'src/environments/environment';
import { catchError, map } from 'rxjs/operators';

import { ResponseApi } from '../models/response_api.model';
import { Subject } from 'rxjs';
import { Client } from '../models/Clients';

@Injectable({
  providedIn: 'root'
})
export class ClientService {
  private cliente$ = new Subject<any>();
  // Paths
  private path: string = '/Client';

  constructor(private http: HttpClient, public router: Router) { }


  getAll(): Observable<ResponseApi> {
    //console.log('aqui');
    return this.http
      .get<ResponseApi>(
        `${environment.urlApi.environment}${this.path}`
      )
      .pipe(map((result: ResponseApi) => {
        //console.log(result);
        return result;
      }),
        catchError((error) => {
          return throwError(() => error)
        })
      );
  }

  get(id: string): Observable<ResponseApi> {
    return this.http
      .get<ResponseApi>(`${environment.urlApi.environment}${this.path}/${id}`)
      .pipe(
        map((result: ResponseApi) => result),
        catchError((error) => {
          return throwError(() => error)
        })
      );
  }

  post(producto: any): Observable<ResponseApi> {
    return this.http
      .post<ResponseApi>(`${environment.urlApi.environment}${this.path}`, producto)
      .pipe(map((result: ResponseApi) => {
        return result;
      }),
        catchError((error) => {
          return throwError(() => error)
        })
      );
  }

  put(producto: any): Observable<ResponseApi> {
    return this.http
      .put<ResponseApi>(`${environment.urlApi.environment}${this.path}`, producto)
      .pipe(
        map((result: ResponseApi) => result),
        catchError((error) => {
          return throwError(() => error)
        })
      );
  }

  delete(id: any): Observable<ResponseApi> {
    return this.http
      .delete<ResponseApi>(`${environment.urlApi.environment}${this.path}/${id}`)
      .pipe(
        map((result: ResponseApi) => result),
        catchError((error) => {
          return throwError(() => error)
        })
      );
  }

  addClientEdit(client: Client) {
    this.cliente$.next(client);
  }

  getClientEdit(): Observable<Client> {
    return this.cliente$.asObservable();
  }
}
