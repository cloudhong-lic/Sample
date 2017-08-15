import React from 'react';
import classnames from 'classnames';
import PropTypes from 'prop-types';

export default class TodoItem extends React.Component {
	constructor (props) {
		super(props);
	}

	input (isEditing) {
		if (isEditing) {
			const onChange = (event) => this.props.onEditTodo(this.props.todo.id, event.target.value);
			const onStopEditingTodo = this.props.onStopEditingTodo;
			const onKeyDown = (event) => {
				if (event.keyCode === 13) {
					onStopEditingTodo();
				}
			};
			return (
                <input
                    autoFocus={true}
                    className='edit'
                    value={this.props.todo.text}
                    onBlur={onStopEditingTodo}
                    onChange={onChange}
                    onKeyDown={onKeyDown}
                />);
		}

		return null;
	}

	render () {
		const isEditing = this.props.editing === this.props.todo.id;
		const onDeleteTodo = () => this.props.onDeleteTodo(this.props.todo.id);
		const onStartEditingTodo = () => this.props.onStartEditingTodo(this.props.todo.id);
		const onToggleTodo = () => this.props.onToggleTodo(this.props.todo.id);

		return (
            <li
                className={classnames({
                    completed: this.props.todo.complete,
                    editing: isEditing
                })}>
                <div className='view'>
                    <input
                        className='toggle'
                        type='checkbox'
                        checked={this.props.todo.complete}
                        onChange={onToggleTodo}
                    />
                    <label onDoubleClick={onStartEditingTodo}>
                        {this.props.todo.text}
                    </label>
                    <button className='destroy' onClick={onDeleteTodo} />
                </div>
                {this.input(isEditing)}
            </li>
		);
	}
}

// ES5 的写法
TodoItem.propTypes = {
	todo: PropTypes.any.isRequired,
	editing: PropTypes.string.isRequired,
	onDeleteTodo: PropTypes.func.isRequired,
	onStartEditingTodo: PropTypes.func.isRequired,
	onToggleTodo: PropTypes.func.isRequired,
	onEditTodo: PropTypes.func.isRequired,
	onStopEditingTodo: PropTypes.func.isRequired
};
