import { Sistema } from "./Sistema";

export interface Solicitacao {
    
    //Solicitação
    id: number;
    codigo: number;
    acessoEmail: string;
    acessoInternet: boolean;
    dataInicio?: Date;
    status: number;
    statusEmail: number;
    dataSolicitacao?: Date;
    solicitante: string;
    dataValidacao?: Date;
    validador?: string;
    dataCriacao?: Date;
    criador?: string;

    // Colaborador
    nomeCompleto: string;
    idEmpresa: number;
    empresa?: string;
    idCargo: number;
    cargo?: string;
    superior:string;
    tipoContrato:string;
    centroCusto:number;
    sistemas: Sistema[];
    
    // Departamento
    departamento?: string;
    idDepartamento: number;
}