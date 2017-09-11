'use strict';

import { Container } from 'flux/utils';

import AppView from '../components/AppView';
import TodoActions from '../actions/TodoActions';
import TodoDraftStore from '../stores/TodoDraftStore';
import TodoEditStore from '../stores/TodoEditStore';
import TodoStore from '../stores/TodoStore';

function getStores () {
	return [
		TodoDraftStore,
		TodoEditStore,
		TodoStore
	];
}

function getState () {
	return {
		// Store state
		draft: TodoDraftStore.getState(),
		editing: TodoEditStore.getState(),
		todos: TodoStore.getState(),

		// Action state
		onAdd: TodoActions.addTodo,
		onDeleteCompletedTodos: TodoActions.deleteCompletedTodos,
		onDeleteTodo: TodoActions.deleteTodo,
		onEditTodo: TodoActions.editTodo,
		onStartEditingTodo: TodoActions.startEditingTodo,
		onStopEditingTodo: TodoActions.stopEditingTodo,
		onToggleAllTodos: TodoActions.toggleAllTodos,
		onToggleTodo: TodoActions.toggleTodo,
		onUpdateDraft: TodoActions.updateDraft
	};
}

export default Container.createFunctional(AppView, getStores, getState);
