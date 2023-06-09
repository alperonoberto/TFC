import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface DialogData {
  title: string;
  files?: string[];
  message: string;
  isGeneralPurposeModal: boolean;
  isBorrando: boolean;
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
  files?: string[];
  isGeneralPurposeModal: boolean;
  isBorrando: boolean;

  constructor(
    @Inject(MatDialogRef) public dialogRef: MatDialogRef<WarningModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData
  ) {}

  ngOnInit() {
    this.isGeneralPurposeModal = this.data.isGeneralPurposeModal;
    this.isBorrando = this.data.isBorrando;
  }

  onConfirm(): void {
    // Close the dialog, return true
    this.dialogRef.close(true);
  }

  onDismiss(): void {
    // Close the dialog, return false
    this.dialogRef.close(false);
  }
}
