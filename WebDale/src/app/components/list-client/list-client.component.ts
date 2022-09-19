import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Client } from 'src/app/models/Clients';
import { FilterPaginate } from 'src/app/models/filter_paginate.model';
import { FilterPaginateResult } from 'src/app/models/response_api_filter_paginate.model';
import { ClientService } from 'src/app/services/client.service';

@Component({
  selector: 'app-list-client',
  templateUrl: './list-client.component.html',
  styleUrls: ['./list-client.component.css']
})
export class ListClientComponent implements OnInit {
  listClient: Client[] = [];

  constructor(private _clientService: ClientService, private toastr: ToastrService, public router: Router) { }

  ngOnInit(): void {
    console.log('ingresa');
    this.getClient();
  }

  public goToPage(page: number): void {
    this.getClient();
  }

  getClient() {
    this._clientService.getAll().subscribe((response) => {
      console.log(response);
      if (response.information != null) {
        //console.log(response.information);
        this.listClient = response.information as Client[];
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

  getClientByID(id: string) {
    this._clientService.get(id).subscribe((data) => {
      this.listClient = data.information as Client[];
    });
  }

  deleteClient(id: any) {
    this._clientService.delete(id).subscribe((response) => {
      if (response.state) {
        this.toastr.info(response.message?.join(','), 'Información!')
      }
      else {
        this.toastr.error(response.message?.join(','), 'Error');
      }
    });

    this.getClient();
  }

  editarClient(client: Client) {
    this._clientService.addClientEdit(client);
  }

}
