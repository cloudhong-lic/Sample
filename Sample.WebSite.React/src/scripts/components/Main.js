import React from 'react';
import PropTypes from 'prop-types';

import TodoItem from './TodoItem';

export default class Main extends React.Component {
	constructor (props) {
		super(props);
	}

	render () {
		if (this.props.todos.size === 0) {
			return null;
		}

        // If this were expensive we could move it to the container.
		const areAllComplete = this.props.todos.every(todo => todo.complete);

		return (
            <section id='main'>
                <input
                    checked={areAllComplete ? 'checked' : ''}
                    id='toggle-all'
                    type='checkbox'
                    onChange={this.props.onToggleAllTodos}
                />
                <label htmlFor='toggle-all'>
                    Mark all as complete
      </label>
                <ul id='todo-list'>
                    {[...this.props.todos.values()].reverse().map(todo => (
                        <TodoItem
                            key={todo.id}
                            editing={this.props.editing}
                            todo={todo}
                            onDeleteTodo={this.props.onDeleteTodo}
                            onEditTodo={this.props.onEditTodo}
                            onStartEditingTodo={this.props.onStartEditingTodo}
                            onStopEditingTodo={this.props.onStopEditingTodo}
                            onToggleTodo={this.props.onToggleTodo}
                        />
                    ))}
                </ul>
            </section>
		);
	}
}

// ES5 的写法
Main.propTypes = {
	todos: PropTypes.any.isRequired,
	editing: PropTypes.string.isRequired,
	onDeleteTodo: PropTypes.func.isRequired,
	onEditTodo: PropTypes.func.isRequired,
	onStartEditingTodo: PropTypes.func.isRequired,
	onStopEditingTodo: PropTypes.func.isRequired,
	onToggleTodo: PropTypes.func.isRequired,
	onToggleAllTodos: PropTypes.func.isRequired
};

Main.defaultProps = {
	todos: [],
	editing: '',
	onDeleteTodo: () => null,
	onEditTodo: () => null,
	onStartEditingTodo: () => null,
	onStopEditingTodo: () => null,
	onToggleTodo: () => null,
	onToggleAllTodos: () => null
};
