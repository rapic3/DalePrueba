import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

import { FilterPaginate } from '../models/filter_paginate.model';
import { ResponseApi } from '../models/response_api.model';
import { ResponseApiFilterPaginate } from '../models/response_api_filter_paginate.model';
import { throwError } from 'rxjs/internal/observable/throwError';
import { environment } from 'src/environments/environment';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  // Paths
  private path: string = '/Order';

  constructor(private http: HttpClient, public router: Router) { }

  getAll(filter: FilterPaginate): Observable<ResponseApiFilterPaginate> {
    //console.log('aqui');
    return this.http
      .get<ResponseApi>(
        `${environment.urlApi.order}${this.path}?Page=${filter.page ?? 1
        }&SortBy=${filter.sortBy ?? ''}&pageSize=${filter.pageSize ?? ''}&Search=${filter.search ?? ''}&Status=${filter.filterStatus ?? ''
        }`
      )
      .pipe(map((result: ResponseApiFilterPaginate) => {
        return result;
      }),
        catchError((error) => {
          return throwError(() => error)
        })
      );
  }

  get(id: string): Observable<ResponseApi> {
    return this.http
      .get<ResponseApi>(`${environment.urlApi.order}${this.path}/${id}`)
      .pipe(
        map((result: ResponseApi) => result),
        catchError((error) => {
          return throwError(() => error)
        })
      );
  }

  post(order: any): Observable<ResponseApi> {
    return this.http
      .post<ResponseApi>(`${environment.urlApi.order}${this.path}`, order)
      .pipe(map((result: ResponseApi) => {
        return result;
      }),
        catchError((error) => {
          return throwError(() => error)
        })
      );
  }

  put(order: any): Observable<ResponseApi> {
    return this.http
      .put<ResponseApi>(`${environment.urlApi.order}${this.path}`, order)
      .pipe(
        map((result: ResponseApi) => result),
        catchError((error) => {
          return throwError(() => error)
        })
      );
  }

  delete(id: any): Observable<ResponseApi> {
    return this.http
      .delete<ResponseApi>(`${environment.urlApi.order}${this.path}/${id}`)
      .pipe(
        map((result: ResponseApi) => result),
        catchError((error) => {
          return throwError(() => error)
        })
      );
  }
}
