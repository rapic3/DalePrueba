import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import {NgbPaginationModule} from '@ng-bootstrap/ng-bootstrap';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CreateProductComponent } from './components/create-product/create-product.component';
import { ListProductComponent } from './components/list-product/list-product.component';
import { ListClientComponent } from './components/list-client/list-client.component';
import { CreateClientComponent } from './components/create-client/create-client.component';
import { PaginatorAppComponent} from './components/paginator-app/paginator-app.component'

import { ServicesIndexModule } from './services/services-index.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ProductComponent } from './components/product/product.component';
import { HomeComponent } from './components/home/home.component';

import { LayoutModule } from './layout/layout.module';
import { ClientComponent } from './components/client/client.component';
import { OrderComponent } from './components/order/order.component';
import { CreateOrderComponent } from './components/create-order/create-order.component';

@NgModule({
  declarations: [
    AppComponent,
    CreateProductComponent,
    ListProductComponent,
    ListClientComponent,
    CreateClientComponent,
    PaginatorAppComponent,
    ProductComponent,
    HomeComponent,
    ClientComponent,
    OrderComponent,
    CreateOrderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    ServicesIndexModule,
    NgbModule,
    NgbPaginationModule,
    LayoutModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
