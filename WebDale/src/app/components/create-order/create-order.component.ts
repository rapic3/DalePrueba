import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Client } from 'src/app/models/Clients';
import { ItemOrderRequest } from 'src/app/models/ItemsOrder';
import { OrderRequest } from 'src/app/models/Orders';
import { Product } from 'src/app/models/Products';
import { ClientService } from 'src/app/services/client.service';
import { OrderService } from 'src/app/services/order.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-create-order',
  templateUrl: './create-order.component.html',
  styleUrls: ['./create-order.component.css']
})
export class CreateOrderComponent implements OnInit {
  form: FormGroup;
  loading = false;
  titulo = '';
  id: string | undefined;
  listClient: Client[] = [];
  product: any;

  constructor(private fb: FormBuilder, private _orderService: OrderService, private _clientService: ClientService, private _productService: ProductService, private toastr: ToastrService, public router: Router, private routInfo: ActivatedRoute) {
    this.form = this.fb.group(
      {
        value: ['', Validators.required],
        items: ['', [Validators.required]],
        clientId: ['', [Validators.required]],
        factura: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(16)]],
      }
    )

    this._productService.get(this.routInfo.snapshot.params['id']).subscribe((response) => {
      this.product = response.information;
    });
  }

  ngOnInit(): void {
    this.onLoadClient();
    this.titulo = this.product?.name;
    //console.log(this.routInfo.snapshot.params['id']);
  }

  onLoadClient() {
    //alert('aqui');
    this._clientService.getAll().subscribe((result) => {
      this.listClient = result.information as Client[];
      //console.log(this.listClient);
    });
  }

  createOrder(): void {
    //console.log(this.product);
    if (this.product !== undefined) {
      const Item: ItemOrderRequest = {
        price: this.product.value,
        productId: this.product.id,
        quantity: 1
      };

      const ORDER: OrderRequest = {
        value: this.product.value,
        items: [Item],
        clientId: this.form.value.clientId,
        factura: this.form.value.factura,
      }

      console.log(ORDER);
      this.loading = true;
      this._orderService.post(ORDER).subscribe(response => {
        if (response.state) {
          this.loading = false;
          console.log('orden registrada');
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
    else {
      this.loading = false;
      this.toastr.error('No se encontro el producto', 'Error');
    }
  }
}
