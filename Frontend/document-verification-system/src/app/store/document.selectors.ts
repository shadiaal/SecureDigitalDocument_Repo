import { createSelector, createFeatureSelector } from '@ngrx/store';
import { DocumentState, RootState } from './document.reducer';

export const selectDocumentState = createFeatureSelector<RootState, DocumentState>('document');

export const selectDocuments = createSelector(selectDocumentState, (state) => state.documents);
export const selectLoading = createSelector(selectDocumentState, (state) => state.loading);
export const selectError = createSelector(selectDocumentState, (state) => state.error);

