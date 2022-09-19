import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FilterPaginate } from 'src/app/models/filter_paginate.model';
import { Product } from 'src/app/models/Products';
import { FilterPaginateResult } from 'src/app/models/response_api_filter_paginate.model';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  listProduct: Product[] = [];
  filter: FilterPaginate = { page: 1, pageSize: 6 };
  paginate: FilterPaginateResult | undefined;

  constructor( private _productService: ProductService, private toastr: ToastrService, public router: Router) { 

  }

  ngOnInit(): void {
    this.getProduct();
  }

  public goToPage(page: number): void {
    this.filter.page = page;
    this.getProduct();
  }

  getProduct() {
    this._productService.getAll(this.filter).subscribe((response) => {
      //console.log(response);
      if (response.information != null) {
        console.log(response.information);
        this.listProduct = response.information.items as Product[];
        this.paginate = {
          totalItems: response.information.totalItems,
          pageLenght: response.information.pageLenght,
          pageSize: response.information.pageSize,
          items: response.information.items,
        };
      }
      else {
        //console.log(response.message);
        this.toastr.error(response.message?.join(','), 'Información!',
          {
            timeOut: 2000,
          });
      }
    });
  }

}
