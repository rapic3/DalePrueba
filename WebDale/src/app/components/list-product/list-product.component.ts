import { Component, OnInit } from '@angular/core';
import { FilterPaginate } from 'src/app/models/filter_paginate.model';
import { Product } from 'src/app/models/Products';
import { FilterPaginateResult } from 'src/app/models/response_api_filter_paginate.model';
import { ProductService } from 'src/app/services/product.service';

import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-list-product',
  templateUrl: './list-product.component.html',
  styleUrls: ['./list-product.component.css']
})
export class ListProductComponent implements OnInit {
  listProduct: Product[] = [];
  filter: FilterPaginate = { page: 1, pageSize: 5 };
  paginate: FilterPaginateResult | undefined;

  constructor(private _productService: ProductService, private toastr: ToastrService, public router: Router) { }

  ngOnInit(): void {
    console.log('ingresa');
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
        //console.log(response.information);
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

  getProductByID(id: string) {
    this._productService.get(id).subscribe((data) => {
      this.listProduct = data.information as Product[];
    });
  }

  deleteProduct(id: any) {
    this._productService.delete(id).subscribe((response) => {
      if (response.state) {
        this.toastr.info(response.message?.join(','), 'Información!')
      }
      else {
        this.toastr.error(response.message?.join(','), 'Error');
      }
    });

    this.getProduct();
  }

  editarProducto(product: Product) {
    this._productService.addProductEdit(product);
  }
}
