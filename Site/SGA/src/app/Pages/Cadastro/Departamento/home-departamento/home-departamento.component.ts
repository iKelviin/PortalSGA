import { animate, query, style, transition, trigger } from '@angular/animations';
import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Departamento } from '../../../../Models/Departamento';
import { DisplayColumn } from '../../../../Models/DisplayColumn';
import { DepartamentoService } from '../../../../Services/departamento.service';
import { AlertService } from '../../../../Shared/Services/alert.service';
import { MatDialog } from '@angular/material/dialog';
import { HotToastService } from '@ngneat/hot-toast';
import { ModalDepartamentoComponent } from '../modal-departamento/modal-departamento.component';
import { catchError, of } from 'rxjs';
import { Empresa } from '../../../../Models/Empresa';
import { EmpresaService } from '../../../../Services/empresa.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-home-departamento',
  templateUrl: './home-departamento.component.html',
  styleUrl: './home-departamento.component.scss',
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
export class HomeDepartamentoComponent implements OnInit{
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  ELEMENT_DATA!: Departamento[];
  dataSource = new MatTableDataSource<Departamento>(this.ELEMENT_DATA);
  dataSourceFiltered: Departamento[] = [];
  lstEmpresas = new MatTableDataSource<Empresa>();
  add: string = 'Cadastrar';
  edit: string = 'Editar';
  delete: string = 'Deletar';
  isLoading: boolean = true;
  selection!: SelectionModel<Departamento>;
  cboEmpresaSelecionada: string = 'all';

  displayedColumns: DisplayColumn[] = [
    { def: 'select', label: 'Seleção', hide: false, export: false},
    { def: 'id', label: 'ID', hide: true, export: true },
    { def: 'nomeEmpresa', label: 'Empresa', hide: true, export: false },
    { def: 'nome', label: 'Departamento', hide: false, export: true },
    { def: 'centroCusto', label: 'Centro de Custo', hide: false, export: true },
    { def: 'action', label: 'Ação', hide: false, export: false }
  ];

  public tableForm: FormGroup = this.formBuilder.group({
    idEmpresa: ['']
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

  // Used in the template
  disColumns!: string[];

  checkBoxList: DisplayColumn[] = [];
  
  constructor(
    private departamentoService: DepartamentoService,
    private empresaService: EmpresaService,
    public dialog: MatDialog,
    private alertService: AlertService,
    private toast: HotToastService,
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private route: Router
    ) {}

  ngOnInit(): void {
    // Apply paginator
    this.dataSource.paginator = this.paginator;

    // Apply sort option
    this.dataSource.sort = this.sort;

    this.selection = new SelectionModel<Departamento>(true, []);

    // Update with columns to be displayed
    this.disColumns = this.displayedColumns.filter(cd => !cd.hide).map(cd => cd.def)

    
    this.activatedRoute.params.subscribe(params => {
      const idEmpresa = params['idEmpresa'];
      console.log(idEmpresa);
        if(idEmpresa !== undefined){
          this.cboEmpresaSelecionada = idEmpresa;
          this.empresaSelecionada.id = idEmpresa;
          this.tableForm.get('idEmpresa')?.setValue(idEmpresa);
          
        }else{
          this.cboEmpresaSelecionada = 'all'

        }
      });

    this.listarEmpresas();
    this.listarDepartamentos();
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

  listarDepartamentos(){
    if(this.cboEmpresaSelecionada == 'all'){
      let resp = this.departamentoService.getAll().pipe(
        catchError(error =>{
          this.toast.error('Erro ao carregar os departamentos');
          return of([])
        }));
      setTimeout(() => {
        resp.subscribe((report) => {
          this.isLoading = false;
          this.dataSource.data = report as Departamento[];
          this.dataSourceFiltered = this.dataSource.data;
        });
      }, 1000);
    }else{
      let resp = this.departamentoService.getAllByEmpresa(this.empresaSelecionada.id).pipe(
        catchError(error =>{
          this.toast.error('Erro ao carregar os departamentos');
          return of([])
        }));
      setTimeout(() => {
        resp.subscribe((report) => {
          this.isLoading = false;
          this.dataSource.data = report as Departamento[];
          this.dataSourceFiltered = this.dataSource.data;
        });
      }, 1000);
    }
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

  openAddEditDialog(action: string, obj: any): void {
    obj.action = action;
    const dialogRef = this.dialog.open(ModalDepartamentoComponent, {
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
          this.listarDepartamentos();

        } else if (action == this.edit) {
          this.updateRowData(result.data);
          this.toast.success('Editado com sucesso!');
          this.ClearSelection();
          this.listarDepartamentos();
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
    this.departamentoService.post(row_obj.data).subscribe(
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
    this.departamentoService.post(row_obj.data).subscribe(
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

  deleteRows(rows: Departamento[] | Departamento): void {

    if (Array.isArray(rows)) {
      rows.forEach(row => {
        this.deleteRow(row);
      });
    } else {
      this.deleteRow(rows);
    }
  }
  
  // Delete a single row by 'row' delete button
  deleteRow(row: Departamento): void {
    const index = this.dataSource.data.findIndex((item) => item.id === row.id);
    if (index > -1) {
      
      this.departamentoService.delete(row).pipe(
        this.toast.observe({
          loading: 'Excluindo Departamento...',
          success: 'Departamento excluido com sucesso!',
          error: 'Falha ao excluir o departamento.'
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
openDeleteDialog(len: number, rows: Departamento[]): void {
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
      this.listarDepartamentos();
      this.ClearSelection();
    }
  });
}

openViewChildDialog(len: number, rows: Departamento | Departamento[]): void {
  const options = {
    title: 'Visualizar Cargos?',
    message: `Gostaria de visualizar os Cargos deste Departamento?`,
    cancelText: 'Não',
    confirmText: 'Sim'
  };

  // If user confirms, remove selected rows from data table
  this.alertService.open(options);
  this.alertService.confirmed().subscribe(confirmed => {
    if (confirmed) {
      const idDepartamento = Array.isArray(rows) ? rows[0].id : rows.id;
      const idEmpresa = this.empresaSelecionada.id;
      this.route.navigateByUrl(`empresas/${idEmpresa}/departamentos/${idDepartamento}/cargos`);
    }
  });
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

      resp.subscribe((produtos: Departamento[]) => {
        this.dataSource.data = produtos.filter(produtos => produtos.idEmpresa === idEmpresa);
      });

}



}
