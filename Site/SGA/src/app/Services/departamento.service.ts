import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Departamento } from '../Models/Departamento';
import { Observable, map } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DepartamentoService {

  private baseURL = environment.baseApiUrl;
  private endpointEmpresa = 'empresas';
  private endpointDepartamento = 'departamentos';


  constructor(private http: HttpClient) { }

  getAll():Observable<Departamento[]>{
    return this.http.get<Departamento[]>(`${this.baseURL}/${this.endpointEmpresa}/${this.endpointDepartamento}`);
  }

  getAllByEmpresa(idEmpresa:number):Observable<Departamento[]>{
    return this.http.get<Departamento[]>(`${this.baseURL}/${this.endpointEmpresa}/${idEmpresa}/${this.endpointDepartamento}`);
  }

  get(id: string): Observable<Departamento> {
    return this.http.get<Departamento>(`${this.baseURL}/${this.endpointDepartamento}/${id}`);
  }

  post(Departamento: Departamento): Observable<Departamento>{
    return this.http.post<Departamento>(`${this.baseURL}/${this.endpointEmpresa}/${Departamento.idEmpresa}/${this.endpointDepartamento}`, Departamento)
            .pipe(map(s => s));
  }

  delete(Departamento: Departamento): Observable<Departamento>{
    return this.http.delete<Departamento>(`${this.baseURL}/${this.endpointEmpresa}/${this.endpointDepartamento}/${Departamento.id}`)
            .pipe(map(s => s));
  }


}

