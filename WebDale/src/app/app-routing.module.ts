import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProductComponent } from './components/product/product.component';
import { HomeComponent } from './components/home/home.component';
import { ClientComponent } from './components/client/client.component';
import { OrderComponent } from './components/order/order.component';
import { CreateOrderComponent } from './components/create-order/create-order.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'inicio',
        component: HomeComponent,
        data: { showRootComponents: true },
      },
      {
        path: 'productos',
        component: ProductComponent,
        data: { showRootComponents: true },
      },
      {
        path: 'clientes',
        component: ClientComponent,
        data: { showRootComponents: true },
      },
      {
        path: 'ordenes',
        component: OrderComponent,
        data: { showRootComponents: true },
      },
      {
        path: 'crearordenes/:id',
        component: CreateOrderComponent,
        data: { showRootComponents: true },
      },
      { path: '', redirectTo: '/inicio', pathMatch: 'full' },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
