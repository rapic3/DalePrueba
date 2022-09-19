import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

// Services
import { ProductService } from './product.service';


@NgModule({
  declarations: [],
  imports: [CommonModule, HttpClientModule],
  providers: [
    ProductService,
  ],
})
export class ServicesIndexModule { }
