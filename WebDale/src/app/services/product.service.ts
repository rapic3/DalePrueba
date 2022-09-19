import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import { Observable } from 'rxjs/internal/Observable';
import { throwError } from 'rxjs/internal/observable/throwError';
import { environment } from 'src/environments/environment';
import { catchError, map } from 'rxjs/operators';

import {
  FilterPaginate,
} from '../models/filter_paginate.model';

import {
  ResponseApiFilterPaginate,
} from '../models/response_api_filter_paginate.model';

import {
  ResponseApi,
} from '../models/response_api.model';
import { Product } from '../models/Products';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private producto$ = new Subject<any>();
  // Paths
  private path: string = '/Product';

  constructor(private http: HttpClient, public router: Router) { }


  getAll(filter: FilterPaginate): Observable<ResponseApiFilterPaginate> {
    //console.log('aqui');
    return this.http
      .get<ResponseApi>(
        `${environment.urlApi.product}${this.path}?Page=${filter.page ?? 1
        }&SortBy=${filter.sortBy ?? ''}&pageSize=${filter.pageSize ?? ''}&Search=${filter.search ?? ''}&Status=${filter.filterStatus ?? ''
        }&SortProduct=${filter.sortProduct ?? ''
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
      .get<ResponseApi>(`${environment.urlApi.product}${this.path}/${id}`)
      .pipe(
        map((result: ResponseApi) => {
          console.log(result);
          return result
        }),
        catchError((error) => {
          return throwError(() => error)
        })
      );
  }

  post(producto: any): Observable<ResponseApi> {
    return this.http
      .post<ResponseApi>(`${environment.urlApi.product}${this.path}`, producto)
      .pipe(map((result: ResponseApi) => {
        return result;
      }),
        catchError((error) => {
          return throwError(() => error)
        })
      );
  }

  put(producto: any): Observable<ResponseApi> {
    console.log('Update');
    return this.http
      .put<ResponseApi>(`${environment.urlApi.product}${this.path}`, producto)
      .pipe(
        map((result: ResponseApi) => {
          console.log(result);
          return result
        }),
        catchError((error) => {
          return throwError(() => error)
        })
      );
  }

  delete(id: any): Observable<ResponseApi> {
    return this.http
      .delete<ResponseApi>(`${environment.urlApi.product}${this.path}/${id}`)
      .pipe(
        map((result: ResponseApi) => result),
        catchError((error) => {
          return throwError(() => error)
        })
      );
  }

  addProductEdit(product: Product) {
    this.producto$.next(product);
  }

  getProductEdit(): Observable<Product> {
    return this.producto$.asObservable();
  }
}