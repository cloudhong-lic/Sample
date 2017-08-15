import React from 'react';
import PropTypes from 'prop-types';

export default class NewTodo extends React.Component {
	constructor (props) {
		super(props);
	}

	render () {
		const addTodo = () => this.props.onAdd(this.props.draft);
		const onBlur = () => addTodo();
		const onChange = (event) => this.props.onUpdateDraft(event.target.value);
		const onKeyDown = (event) => {
			if (event.keyCode === 13) {
				addTodo();
			}
		};

		return (
            <input
                autoFocus={true}
                id='new-todo'
                placeholder='What needs to be done?'
                value={this.props.draft}
                onBlur={onBlur}
                onChange={onChange}
                onKeyDown={onKeyDown}
            />
		);
	}
}

// ES5 的写法
NewTodo.propTypes = {
	draft: PropTypes.string.isRequired,
	onAdd: PropTypes.func.isRequired,
	onUpdateDraft: PropTypes.func.isRequired
};
