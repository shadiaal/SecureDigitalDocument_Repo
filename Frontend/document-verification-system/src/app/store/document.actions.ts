import { createAction, props } from '@ngrx/store';

export const loadDocuments = createAction('[Document] Load Documents');
export const loadDocumentsSuccess = createAction(
  '[Document] Load Documents Success',
  props<{ documents: any[] }>()
);
export const loadDocumentsFailure = createAction(
  '[Document] Load Documents Failure',
  props<{ error: string }>()
);

