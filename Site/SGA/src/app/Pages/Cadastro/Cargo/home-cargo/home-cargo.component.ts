import { Component, OnInit, ViewChild } from '@angular/core';
import { animate, query, style, transition, trigger } from '@angular/animations';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Cargo } from '../../../../Models/Cargo';
import { Empresa } from '../../../../Models/Empresa';
import { MatTableDataSource } from '@angular/material/table';
import { Departamento } from '../../../../Models/Departamento';
import { SelectionModel } from '@angular/cdk/collections';
import { DisplayColumn } from '../../../../Models/DisplayColumn';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HotToastService } from '@ngneat/hot-toast';
import { AlertService } from '../../../../Shared/Services/alert.service';
import { MatDialog } from '@angular/material/dialog';
import { EmpresaService } from '../../../../Services/empresa.service';
import { DepartamentoService } from '../../../../Services/departamento.service';
import { catchError, of } from 'rxjs';
import { ModalCargoComponent } from '../modal-cargo/modal-cargo.component';
import { CargoService } from '../../../../Services/cargo.service';


@Component({
  selector: 'app-home-cargo',
  templateUrl: './home-cargo.component.html',
  styleUrl: './home-cargo.component.scss',
  animations: [
    trigger('animation', [
      transition('* => *', [
        query(
          ':enter',
          [
            style({ transform: 'translateX(100%)', opacity: 0 }),
            animate('500ms', style({ transform: 'translateX(0)', opacity: 1 }))
          ],
          { optional: true }
        ),
        query(
          ':leave',
          [
            style({ transform: 'translateX(0)', opacity: 1 }),
            animate('500ms', style({ transform: 'translateX(100%)', opacity: 0 }))
          ],
          {
            optional: true
          }
        )
      ])
    ])]
})
export class HomeCargoComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  ELEMENT_DATA!: Cargo[];
  dataSource = new MatTableDataSource<Cargo>(this.ELEMENT_DATA);
  dataSourceFiltered: Cargo[] = [];
  lstEmpresas = new MatTableDataSource<Empresa>();
  lstDepartamentos = new MatTableDataSource<Departamento>();
  add: string = 'Cadastrar';
  edit: string = 'Editar';
  delete: string = 'Deletar';
  isLoading: boolean = true;
  selection!: SelectionModel<Cargo>;
  cboEmpresaSelecionada: string = 'all';
  cboDepartamentoSelecionado: string = 'all';
  
  displayedColumns: DisplayColumn[] = [
    { def: 'select', label: 'Seleção', hide: false, export: false},
    { def: 'id', label: 'ID', hide: true, export: true },
    { def: 'nome', label: 'Cargo', hide: false, export: true },
    { def: 'nomeEmpresa', label: 'Empresa', hide: true, export: true },
    { def: 'nomeDepartamento', label: 'Departamento', hide: false, export: true },
    { def: 'action', label: 'Ação', hide: false, export: false }
  ];
  
  // Used in the template
  disColumns!: string[];

  checkBoxList: DisplayColumn[] = [];
  
  constructor(
    private cargoService: CargoService,
    private departamentoService: DepartamentoService,
    private empresaService: EmpresaService,
    public dialog: MatDialog,
    private alertService: AlertService,
    private toast: HotToastService,
    private formBuilder: FormBuilder,
    private route: Router,
    private activatedRoute: ActivatedRoute,
    ) {}

    
  public tableForm: FormGroup = this.formBuilder.group({
    idEmpresa: [''],
    idDepartamento: ['']
  })
  
  empresaSelecionada: Empresa = {
    id: 0,
    nome: '',
    email: '',
    telefone: '',
    cep: '',
    endereco: '',
    numero: 0,
    complemento: '',
    bairro: '',
    cidade: '',
    estado: '',
    ativo: false
  }

  departamentoSelecionado: Departamento = {
    id: 0,
    nome: '',
    centroCusto: 0,
    idEmpresa: 0,
    nomeEmpresa: 0
  }
  
  
  ngOnInit(): void {
    // Apply paginator
    this.dataSource.paginator = this.paginator;

    // Apply sort option
    this.dataSource.sort = this.sort;

    this.selection = new SelectionModel<Cargo>(true, []);

    // Update with columns to be displayed
    this.disColumns = this.displayedColumns.filter(cd => !cd.hide).map(cd => cd.def)
    
    this.listarEmpresas();        
    
    this.activatedRoute.params.subscribe(params => {
      const idEmpresa = params['idEmpresa'];
      const idDepartamento = params['idDepartamento'];    
        if(idDepartamento !== undefined && idEmpresa !== undefined){
          this.cboDepartamentoSelecionado = idDepartamento;
          this.departamentoSelecionado.id = idDepartamento;
          this.tableForm.get('idDepartamento')?.setValue(idDepartamento);

          this.cboEmpresaSelecionada = idEmpresa;
          this.empresaSelecionada.id = idEmpresa;
          this.tableForm.get('idEmpresa')?.setValue(idEmpresa);
          
        }else{
          this.cboDepartamentoSelecionado = 'all'
          this.cboEmpresaSelecionada = 'all'
          
        }
      });     
      
      this.listarDepartamentos();
      this.listarCargos();
  }

  LimparFiltros(){
    this.empresaSelecionada.id = 0;
    this.departamentoSelecionado.id =  0;
    this.tableForm.get('idEmpresa')?.setValue(0);
    this.tableForm.get('idDepartamento')?.setValue(0);
    this.listarCargos();
  }

  listarDepartamentos(){
    let resp = this.departamentoService.getAll().pipe(
      catchError(error =>{
        this.toast.error('Erro ao carregar os departamentos.');
        return of([])
      }));

      resp.subscribe((report) => {
        this.lstDepartamentos.data = report as Departamento[];
        this.lstDepartamentos.data = this.lstDepartamentos.data.filter(x=> x.idEmpresa == this.empresaSelecionada.id);        
      });
  }


  listarEmpresas(){
    let resp = this.empresaService.getAll().pipe(
      catchError(error =>{
        this.toast.error('Erro ao carregar as empresas');
        return of([])
      }));

      resp.subscribe((report) => {
        this.lstEmpresas.data = report as Empresa[];
        this.lstEmpresas.data = this.lstEmpresas.data.filter(x=> x.ativo === true);        
      });
  }

  applyFilter(event: any): void {
    this.dataSource.filter = event.target.value.trim().toLowerCase();
  }

  // This function will be called when user click on select all check-box
  isAllSelected(): boolean {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  masterToggle(): void {
    this.isAllSelected()
      ? this.selection.clear()
      : this.dataSource.data.forEach(row => this.selection.select(row));
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  toggleAllRows() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.dataSource.data);
  }

  
  // Show/Hide check boxes
  showCheckBoxes(): void {
    this.checkBoxList = this.displayedColumns;
  }

  hideCheckBoxes(): void {
    this.checkBoxList = [];
  }

  toggleForm(): void {
    this.checkBoxList.length ? this.hideCheckBoxes() : this.showCheckBoxes();
  }

  hideColumn(event: any, item: string) {
    this.displayedColumns.forEach(element => {
      if (element['def'] == item) {
        element['hide'] = event.checked;
      }
    });
    this.disColumns = this.displayedColumns.filter(cd => !cd.hide).map(cd => cd.def)
  }

  ClearSelection(){
    this.selection.clear();
    return;
  }

  public onChangeEmpresa(event: any){
    const selectElement = event.target as HTMLSelectElement;
    const value = selectElement.value;
    const idEmpresa = parseInt(value);
    this.empresaSelecionada.id = idEmpresa
    let resp = this.departamentoService.getAllByEmpresa(idEmpresa).pipe(
      catchError(error =>{
        this.toast.error('Erro ao carregar os departamentos desta empresa.');
        return of([])
      }));

      resp.subscribe((departamentos: Departamento[]) => {
        this.lstDepartamentos.data = departamentos.filter(d => d.idEmpresa === idEmpresa);
      });
  }

  public onChangeDepartamento(event: any){
    const selectElement = event.target as HTMLSelectElement;
    const value = selectElement.value;
      const idDepartamento = parseInt(value);
      this.departamentoSelecionado.id = idDepartamento
      let resp = this.cargoService.getAllByDepartamento(idDepartamento).pipe(
        catchError(error =>{
          this.toast.error('Erro ao carregar os cargos deste departamento.');
          return of([])
        }));
  
        resp.subscribe((cargos: Cargo[]) => {
          this.dataSource.data = cargos.filter(c => c.idDepartamento === idDepartamento);
        });
  
  }


  openAddEditDialog(action: string, obj: any): void {
    obj.action = action;
    console.log(obj);
    const dialogRef = this.dialog.open(ModalCargoComponent, {
      data: obj,
      enterAnimationDuration: '300ms',
      exitAnimationDuration: '500ms'
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result != null) {
        const action = result.data['action'];
        delete result.data['action'];
        if (action == this.add) {
          if(result.data.ativo === null || result.data.ativo === undefined || result.data.ativo === ''){
            result.data.ativo = false;
          }
          this.addRowData(result.data);
          this.toast.success('Cadastrado com sucesso!');
          this.ClearSelection();
          this.listarCargos();

        } else if (action == this.edit) {
          this.updateRowData(result.data);
          this.toast.success('Editado com sucesso!');
          this.ClearSelection();
          this.listarCargos();
        } else {
          console.log(action);
        }
      }
    });
  }

  // Add a row into to data table
  addRowData(row_obj: any): void {
    const data = this.dataSource.data
 
    data.push(row_obj.data);
    this.dataSource.data = data;
    this.cargoService.post(row_obj.data).subscribe(
     (response: any) => {
       if (!String(response.mensagem).includes('Erro')) {
         row_obj.data.id = response.dados;
       }
     },
     (error: any) => {
       console.error('Erro ao adicionar registro:', error);
     }
    );
  }
 
  // Update a row in data table
  updateRowData(row_obj: any): void {
    if (row_obj === null) { return; }
    const data = this.dataSource.data
    this.dataSource.data = data;
    this.cargoService.post(row_obj.data).subscribe(
     {
       next: () => {
         console.log("atualizado com sucesso: ",row_obj.data);
       },
       error: (error) => {
         console.error('Erro ao atualizar a linha:', error);
       }
     }
    );
  }

  deleteRows(rows: Cargo[] | Cargo): void {

    if (Array.isArray(rows)) {
      rows.forEach(row => {
        this.deleteRow(row);
      });
    } else {
      this.deleteRow(rows);
    }
  }
  
  // Delete a single row by 'row' delete button
  deleteRow(row: Cargo): void {
    const index = this.dataSource.data.findIndex((item) => item.id === row.id);
    if (index > -1) {
      
      this.cargoService.delete(row).pipe(
        this.toast.observe({
          loading: 'Excluindo Cargo...',
          success: 'Cargo excluido com sucesso!',
          error: 'Falha ao excluir o cargo.'
        })
       ).subscribe({
        next: () => {
          this.dataSource.data.splice(index, 1);
          this.dataSource.data = [...this.dataSource.data];
        },
        error: (error) => {
          console.error('Erro ao excluir a linha:', error);
        }
      });
    }
  }

    // Open confirmation dialog
openDeleteDialog(len: number, rows: Cargo[]): void {
  const options = {
    title: 'Deletar?',
    message: `Você tem certeza que deseja deletar ${len} linha(s)?`,
    cancelText: 'Não',
    confirmText: 'Sim'
  };

  // If user confirms, remove selected rows from data table
  this.alertService.open(options);
  this.alertService.confirmed().subscribe(confirmed => {
    if (confirmed) {
      this.deleteRows(rows);      
      this.listarCargos();
      this.ClearSelection();
    }
  });
}

listarCargos(){
  if(this.cboDepartamentoSelecionado == 'all'){
    let resp = this.cargoService.getAll().pipe(
      catchError(error =>{
        this.toast.error('Erro ao carregar os cargos');
        return of([])
      }));
    setTimeout(() => {
      resp.subscribe((report) => {
        this.isLoading = false;
        this.dataSource.data = report as Cargo[];
        this.dataSourceFiltered = this.dataSource.data;
      });
    }, 1000);
  }else{
    let resp = this.cargoService.getAllByDepartamento(this.departamentoSelecionado.id).pipe(
      catchError(error =>{
        this.toast.error('Erro ao carregar os cargos');
        return of([])
      }));
    setTimeout(() => {
      resp.subscribe((report) => {
        this.isLoading = false;
        this.dataSource.data = report as Cargo[];
        this.dataSourceFiltered = this.dataSource.data;
      });
    }, 1000);
  }
}


}
