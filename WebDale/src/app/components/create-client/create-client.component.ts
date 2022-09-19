import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ClientRequest, ClientRequestUpdate } from 'src/app/models/Clients';
import { ClientService } from 'src/app/services/client.service';
import { ListClientComponent } from '../list-client/list-client.component';

@Component({
  selector: 'app-create-client',
  templateUrl: './create-client.component.html',
  styleUrls: ['./create-client.component.css']
})
export class CreateClientComponent implements OnInit {
  @ViewChild(ListClientComponent, {static: false})   listClient: ListClientComponent | undefined;
  form: FormGroup;
  loading = false;
  titulo = 'Agregar Cliente';
  id: string = "";

  constructor(private fb: FormBuilder, private _clientService: ClientService, private toastr: ToastrService, public router: Router) { 
    this.form = this.fb.group(
      {
        nombres: ['', [Validators.required, Validators.maxLength(16)]],
        apellidos: ['', [Validators.required, Validators.maxLength(16)]],
        numeroIdentificacion: ['', [Validators.required, Validators.maxLength(16)]],
        direccion: ['', [Validators.required, Validators.maxLength(16)]],
        celular: ['', [Validators.required, Validators.maxLength(16)]],
        email: ['', [Validators.required, Validators.maxLength(16)]],
      }
    )
  }

  ngOnInit(): void {
    this._clientService.getClientEdit().subscribe(data => {
      this.id = data.id;
      this.titulo = 'Editar cliente';
      this.form.patchValue({
        nombres: data.nombres,
        apellidos: data.apellidos,
        numeroIdentificacion: data.numeroIdentificacion,
        direccion: data.direccion,    
        celular: data.numeroIdentificacion,
        email: data.direccion,
      })
    })
  }

  guardarClient() {
    if(this.id === "") {
      this.createClient();
    } else {
      this.updateClient();
    }    
  }

  createClient(): void {
    const CLIENT: ClientRequest = {
      nombres: this.form.value.nombres,
      apellidos: this.form.value.apellidos,
      numeroIdentificacion: this.form.value.numeroIdentificacion,
      direccion: this.form.value.direccion,
      celular: this.form.value.celular,
      email: this.form.value.email,
    }

    this.loading = true;
    this._clientService.post(CLIENT).subscribe(response => {
      if (response.state) {
        this.loading = false;
        console.log('cliente registrado');
        this.listClient?.getClient();
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

  updateClient() {
    const CLIENTE: ClientRequestUpdate = {
      id: this.id,
      nombres: this.form.value.nombres,
      apellidos: this.form.value.apellidos,
      numeroIdentificacion: this.form.value.numeroIdentificacion,
      direccion: this.form.value.direccion,
      celular: this.form.value.celular,
      email: this.form.value.email,
    }
    this.loading = true;
    this._clientService.put(CLIENTE).subscribe(
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
