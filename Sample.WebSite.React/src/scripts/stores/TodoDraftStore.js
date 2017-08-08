'use strict';

import { ReduceStore } from 'flux/utils';
import TodoActionTypes from '../actions/TodoActionTypes';
import TodoDispatcher from '../dispatcher/TodoDispatcher';

class TodoDraftStore extends ReduceStore {
  constructor() {
    super(TodoDispatcher);
  }

  getInitialState() {
    return '';
  }

  reduce(state, action) {
    switch (action.type) {
      case TodoActionTypes.ADD_TODO:
        return '';

      case TodoActionTypes.UPDATE_DRAFT:
        return action.text;

      default:
        return state;
    }
  }
}

export default new TodoDraftStore();
