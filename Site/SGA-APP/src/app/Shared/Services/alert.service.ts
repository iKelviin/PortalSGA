import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { AlertBoxComponent } from '../Components/alert-box/alert-box.component';


@Injectable({
  providedIn: 'root'
})
export class AlertService {

dialogRef!: MatDialogRef<AlertBoxComponent>;

constructor(private dialog: MatDialog) { }

public open(options: any) {
  this.dialogRef = this.dialog.open(AlertBoxComponent, {
    data: {
      title: options.title,
      message: options.message,
      cancelText: options.cancelText,
      confirmText: options.confirmText
    },
  });
}

  public confirmed(): Observable<any> {
    return this.dialogRef.afterClosed().pipe(take(1), map(res => {
      return res;
    }
    ));
  }
}
