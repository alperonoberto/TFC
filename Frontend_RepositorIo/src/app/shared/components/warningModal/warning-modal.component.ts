import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
export interface DialogData {
  title: string;
  message: string;
}

@Component({
  selector: 'app-warning-modal',
  templateUrl: './warning-modal.component.html',
  styleUrls: ['./warning-modal.component.scss'],
})
export class WarningModalComponent implements OnInit {

  dialogData: DialogData;
  title: string;
  message: string;

  constructor(
    public dialogRef: MatDialogRef<WarningModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData
            ) {}

  ngOnInit(): void {}
}
