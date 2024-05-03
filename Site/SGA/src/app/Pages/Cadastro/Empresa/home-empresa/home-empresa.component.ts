import { animate, query, style, transition, trigger } from '@angular/animations';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Empresa } from '../../../../Models/Empresa';
import { catchError, of } from 'rxjs';
import { SelectionModel } from '@angular/cdk/collections';
import { AlertService } from '../../../../Shared/Services/alert.service';
import { EmpresaService } from '../../../../Services/empresa.service';
import { MatDialog } from '@angular/material/dialog';
import { DisplayColumn } from '../../../../Models/DisplayColumn';
import { HotToastService } from '@ngneat/hot-toast';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { ModalEmpresaComponent } from '../modal-empresa/modal-empresa.component';
import { MatRadioChange } from '@angular/material/radio';
import * as XLSX from 'xlsx';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home-empresa',
  templateUrl: './home-empresa.component.html',
  styleUrl: './home-empresa.component.scss',
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
export class HomeEmpresaComponent implements OnInit{

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  ELEMENT_DATA!: Empresa[];
  dataSource = new MatTableDataSource<Empresa>(this.ELEMENT_DATA);
  dataSourceFiltered: Empresa[] = [];
  add: string = 'Cadastrar';
  edit: string = 'Editar';
  delete: string = 'Deletar';
  value: string = '';
  isLoading: boolean = true;
  selection!: SelectionModel<Empresa>;
  selectedEmpresa: string = 'all';

  displayedColumns: DisplayColumn[] = [
    { def: 'select', label: 'Seleção', hide: false, export: false},
    { def: 'id', label: 'ID', hide: false, export: true },
    { def: 'nome', label: 'Nome', hide: false, export: true },
    { def: 'email', label: 'Email', hide: false, export: true },
    { def: 'telefone', label: 'Telefone', hide: true, export: true },
    { def: 'endereco', label: 'Endereço', hide: true, export: true },
    { def: 'numero', label: 'Numero', hide: true, export: true },
    { def: 'cidade', label: 'Cidade', hide: true, export: true },
    { def: 'bairro', label: 'Bairro', hide: true, export: true },
    { def: 'estado', label: 'Estado', hide: true, export: true },
    { def: 'ativo', label: 'Status', hide: false, export: true },
    { def: 'action', label: 'Ação', hide: false, export: false }
  ];

  // Used in the template
  disColumns!: string[];

  checkBoxList: DisplayColumn[] = [];

  constructor(
    private empresaService: EmpresaService,
    public dialog: MatDialog,
    private alertService: AlertService,
    private toast: HotToastService,
    private route: Router
    ) {}

  ngOnInit(): void {
    // Apply paginator
    this.dataSource.paginator = this.paginator;

    // Apply sort option
    this.dataSource.sort = this.sort;

    this.selection = new SelectionModel<Empresa>(true, []);

    // Update with columns to be displayed
    this.disColumns = this.displayedColumns.filter(cd => !cd.hide).map(cd => cd.def)

    this.listarEmpresas();
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
    const dialogRef = this.dialog.open(ModalEmpresaComponent, {
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
          this.listarEmpresas();

        } else if (action == this.edit) {
          this.updateRowData(result.data);
          this.toast.success('Editado com sucesso!');
          this.ClearSelection();
          this.listarEmpresas();
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
   this.empresaService.post(row_obj.data).subscribe(
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
   const index = data.findIndex((item) => item['id'] === row_obj.data['id']);
   if (index > -1) {
     data[index].id = row_obj.data['id'];
     data[index].nome = row_obj.data['nome'];
   }
   this.dataSource.data = data;
   this.empresaService.post(row_obj.data).subscribe(
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

  // Open confirmation dialog
openDeleteDialog(len: number, rows: Empresa[]): void {
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
      this.listarEmpresas();
      this.ClearSelection();
    }
  });
}

deleteRows(rows: Empresa[] | Empresa): void {

  if (Array.isArray(rows)) {
    rows.forEach(row => {
      this.deleteRow(row);
    });
  } else {
    this.deleteRow(rows);
  }
}

// Delete a single row by 'row' delete button
deleteRow(row: Empresa): void {
  const index = this.dataSource.data.findIndex((item) => item.id === row.id);
  if (index > -1) {
    
    this.empresaService.delete(row).pipe(
      this.toast.observe({
        loading: 'Excluindo Empresa...',
        success: 'Empresa excluida com sucesso!',
        error: 'Falha ao excluir a empresa.'
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

openViewChildDialog(len: number, rows: Empresa | Empresa[]): void {
  const options = {
    title: 'Visualizar Departamentos?',
    message: `Gostaria de visualizar os Departamentos desta Empresa?`,
    cancelText: 'Não',
    confirmText: 'Sim'
  };

  // If user confirms, remove selected rows from data table
  this.alertService.open(options);
  this.alertService.confirmed().subscribe(confirmed => {
    if (confirmed) {
      const idApi = Array.isArray(rows) ? rows[0].id : rows.id;
      this.route.navigateByUrl(`departamentos/${idApi}`);
    }
  });
}

openDisableDialog(len: number, rows: Empresa[]): void {
  const options = {
    title: 'Desativar?',
    message: `Você tem certeza que deseja desativar ${len} linha(s)?`,
    cancelText: 'Não',
    confirmText: 'Sim'
  };

  // If user confirms, remove selected rows from data table
  this.alertService.open(options);
  this.alertService.confirmed().subscribe(confirmed => {
    if (confirmed) {
      this.disableRows(rows);
      this.listarEmpresas();
    }
  });
}

disableRows(rows: Empresa[] | Empresa): void {

  if (Array.isArray(rows)) {
    rows.forEach(row => {
      this.disableRow(row);
    });
  } else {
    this.disableRow(rows);
  }
}

// Delete a single row by 'row' delete button
disableRow(row: Empresa): void {
  const index = this.dataSource.data.findIndex((item) => item.id === row.id);
  if (index > -1) {
    row.ativo = false;
    this.empresaService.post(row).pipe(
      this.toast.observe({
        loading: 'Desativando Empresa...',
        success: 'Empresa desativada com sucesso!',
        error: 'Falha ao desativar a empresa.'
      })
     ).subscribe({
      next: () => {
        this.dataSource.data.splice(index, 1);
        this.dataSource.data = [...this.dataSource.data];
      },
      error: (error) => {
        console.error('Erro ao desativar a linha:', error);
      }
    });
  }
}

radioButtonChange(event: MatRadioChange) {
  const option = event.value;
  console.log(option);
  if (option === 'ativos') {
    this.dataSource.data = this.dataSourceFiltered.filter(
      (item) => item.ativo === true
    );
  } else if (option === 'inativos') {
    this.dataSource.data = this.dataSourceFiltered.filter(
      (item) => item.ativo === false
    );
  } else if (option === 'todos') {
    let resp = this.empresaService.getAll().pipe(
      catchError((error) => {
        this.toast.error('Erro ao carregar as empresas.');
        return of([]);
      })
    );
    resp.subscribe((report) => {
      this.isLoading = false;
      this.dataSource.data = report as Empresa[];
      this.dataSourceFiltered = this.dataSource.data;
    });
  }
}

  listarEmpresas(){
    let resp = this.empresaService.getAll().pipe(
      catchError(error =>{
        this.toast.error('Erro ao carregar as empresas');
        return of([])
      }));
    setTimeout(() => {
      resp.subscribe((report) => {
        this.isLoading = false;
        this.dataSource.data = report as Empresa[];
        this.dataSourceFiltered = this.dataSource.data;      
        console.log(this.dataSource.data)  ;
      });
    }, 1000);
  }

  // Fill on selected option
  public onSelectEmpresa(): void {
    this.selection.clear();
    if (this.selectedEmpresa === 'all') {
      this.listarEmpresas();
    } else {
      let resp = this.empresaService.get(this.selectedEmpresa);
      resp.subscribe((report) => { this.dataSource.data = [report] as Empresa[] })
    }
  }


  
//Método de importação Excel


fileName = "";

exportToExcel(): void {
  // Solicita ao usuário que insira o nome do arquivo
  const fileName = window.prompt('Digite o nome do arquivo:');
  if (fileName) {
    const data: Empresa[] = this.dataSource.data;
    const excelData: any[] = [];
    excelData.push(this.displayedColumns.filter(c=> c.export).map(cd => cd.label));

    data.forEach(item => {
      const row = [];
        row.push(item.id);
        row.push(item.nome);
        row.push(item.email);
        row.push(item.telefone);
        row.push(item.endereco);
        row.push(item.numero);
        row.push(item.cidade);
        row.push(item.bairro);
        row.push(item.estado);
        row.push(item.ativo ? 'Ativo' : 'Inativo');
        excelData.push(row);
    });

    const ws: XLSX.WorkSheet = XLSX.utils.aoa_to_sheet(excelData);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

    // Salva o arquivo Excel com o nome fornecido
    XLSX.writeFile(wb, `${fileName}.xlsx`);
  }
}
}





