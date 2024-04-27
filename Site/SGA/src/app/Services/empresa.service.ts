import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Empresa } from '../Models/Empresa';
import { Observable, map } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmpresaService {

  private baseURL = environment.baseApiUrl;
  private endpointEmpresa = 'empresas';


  constructor(private http: HttpClient) { }

  getAll():Observable<Empresa[]>{
    return this.http.get<Empresa[]>(`${this.baseURL}/${this.endpointEmpresa}`);
  }

  get(id: string): Observable<Empresa> {
    return this.http.get<Empresa>(`${this.baseURL}/${this.endpointEmpresa}/${id}`);
  }

  post(empresa: Empresa): Observable<Empresa>{
    return this.http.post<Empresa>(`${this.baseURL}/${this.endpointEmpresa}`, empresa)
            .pipe(map(s => s));
  }

  delete(empresa: Empresa): Observable<Empresa>{
    return this.http.delete<Empresa>(`${this.baseURL}/${this.endpointEmpresa}/${empresa.id}`)
            .pipe(map(s => s));
  }


}

