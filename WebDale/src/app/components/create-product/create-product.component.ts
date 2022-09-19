import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Product, ProductRequest, ProductRequestUpdate } from 'src/app/models/Products';
import { ProductService } from 'src/app/services/product.service';

import { ListProductComponent } from 'src/app/components/list-product/list-product.component';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.css']
})
export class CreateProductComponent implements OnInit {
  @ViewChild(ListProductComponent, {static: false})   listProduct: ListProductComponent | undefined;
  form: FormGroup;
  loading = false;
  titulo = 'Agregar Producto';
  id: string = "";

  constructor(private fb: FormBuilder, private _productoService: ProductService, private toastr: ToastrService, public router: Router) {
    this.form = this.fb.group(
      {
        name: ['', Validators.required],
        referenceCode: ['', [Validators.required, Validators.maxLength(16)]],
        description: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(50)]],
        value: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(16)]],
      }
    )
  }

  ngOnInit(): void {
    this._productoService.getProductEdit().subscribe(data => {
      this.id = data.id;
      this.titulo = 'Editar producto';
      this.form.patchValue({
        name: data.name,
        referenceCode: data.referenceCode,
        description: data.description,
        value: data.value,
      })
    })
  }

  guardarProduct() {
    if(this.id === "") {
      this.createProduct();
    } else {
      this.updateProduct();
    }    
  }

  createProduct(): void {
    const PRODUCTO: ProductRequest = {
      name: this.form.value.name,
      referenceCode: this.form.value.referenceCode,
      description: this.form.value.description,
      value: this.form.value.value,
    }

    this.loading = true;
    this._productoService.post(PRODUCTO).subscribe(response => {
      if (response.state) {
        this.loading = false;
        console.log('producto registrado');
        this.listProduct?.getProduct();
        this.toastr.success(response.message?.join(','), 'Información!')
        this.form.reset();        
      }
      else {
        this.toastr.error(response.message?.join(','), 'Error');
      }
    }, (error: any) => {
      this.loading = false;
      this.toastr.error('Opps.. ocurrio un error', 'Error');
      console.log(error);
    });
  }

  updateProduct() {
    const PRODUCTO: ProductRequestUpdate = {
      id: this.id,
      name: this.form.value.name,
      referenceCode: this.form.value.referenceCode,
      description: this.form.value.description,
      value: this.form.value.value,
    }
    this.loading = true;
    this._productoService.put(PRODUCTO).subscribe(
      (response) => {
        if (response.state) {
          this.loading = false;
          this.toastr.info(response.message?.join(','), 'Información!',
            {
              timeOut: 2000,
            });
        }
        else {
          this.loading = false;
          this.toastr.error(response.message?.join(','), 'Error!',
            {
              timeOut: 2000,
            });
        }
      });
  }
}
