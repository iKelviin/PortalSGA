import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Cargo } from '../Models/Cargo';
import { Observable, map } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CargoService {

  private baseURL = environment.baseApiUrl;
  private endpointEmpresa = 'empresas';
  private endpointDepartamento = 'departamentos';
  private endpointCargo = 'cargos';


  constructor(private http: HttpClient) { }

  getAll():Observable<Cargo[]>{
    return this.http.get<Cargo[]>(`${this.baseURL}/${this.endpointEmpresa}/${this.endpointDepartamento}/${this.endpointCargo}`);
  }

  getAllByDepartamento(idDepartamento:number):Observable<Cargo[]>{
    return this.http.get<Cargo[]>(`${this.baseURL}/${this.endpointEmpresa}/${this.endpointDepartamento}/${idDepartamento}/${this.endpointCargo}`);
  }

  get(id: string): Observable<Cargo> {
    return this.http.get<Cargo>(`${this.baseURL}/${this.endpointEmpresa}/${this.endpointDepartamento}/${this.endpointCargo}/${id}`);
  }

  post(cargo: Cargo): Observable<Cargo>{
    return this.http.post<Cargo>(`${this.baseURL}/${this.endpointEmpresa}/${this.endpointDepartamento}/${cargo.idDepartamento}/${this.endpointCargo}`, cargo)
            .pipe(map(s => s));
  }

  delete(cargo: Cargo): Observable<Cargo>{
    return this.http.delete<Cargo>(`${this.baseURL}/${this.endpointEmpresa}/${this.endpointDepartamento}/${this.endpointCargo}/${cargo.id}`)
            .pipe(map(s => s));
  }


}

