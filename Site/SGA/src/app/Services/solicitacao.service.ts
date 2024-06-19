import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { Observable, map } from 'rxjs';
import { Solicitacao } from '../Models/Solicitacao';

@Injectable({
  providedIn: 'root'
})
export class SolicitacaoService {

  private baseURL = environment.baseApiUrl;
  private endpointSolicitacao = 'solicitacoes';

  constructor(private http: HttpClient){}

  getAll():Observable<Solicitacao[]>{
    return this.http.get<Solicitacao[]>(`${this.baseURL}/${this.endpointSolicitacao}`);
  }

  get(id: string): Observable<Solicitacao> {
    return this.http.get<Solicitacao>(`${this.baseURL}/${this.endpointSolicitacao}/${id}`);
  }

  post(pSolicitacao: Solicitacao): Observable<Solicitacao>{
    return this.http.post<Solicitacao>(`${this.baseURL}/${this.endpointSolicitacao}`, pSolicitacao)
            .pipe(map(s => s));
  }

  delete(pSolicitacao: Solicitacao): Observable<Solicitacao>{
    return this.http.delete<Solicitacao>(`${this.baseURL}/${this.endpointSolicitacao}/${pSolicitacao.id}`)
            .pipe(map(s => s));
  }


}
