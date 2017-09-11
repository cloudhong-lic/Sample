import React from 'react';
import PropTypes from 'prop-types';

export default class Footer extends React.Component {
	constructor (props) {
		super(props);
	}

  // ES6 的写法
  // TODO: 目前webpack config有问题, 暂时不支持这种写法
	// static propTypes = {
	// 	todos: PropTypes.array.isRequired,
	// 	onDeleteCompletedTodos: PropTypes.func.isRequired
	// };

	clearCompletedButton (completed) {
		if (completed > 0) {
			return (
        <button
          id='clear-completed'
          onClick={this.props.onDeleteCompletedTodos}
        >
          Clear completed ({completed})
        </button>);
		}

		return null;
	}

	render () {
		if (this.props.todos.size === 0) {
			return null;
		}

		const remaining = this.props.todos.filter(todo => !todo.complete).size;
		const completed = this.props.todos.size - remaining;
		const phrase = remaining === 1 ? ' item left' : ' items left';

		return (
      <footer id='footer'>
        <span id='todo-count'>
          <strong>
            {remaining}
          </strong>
          {phrase}
        </span>
        {this.clearCompletedButton(completed)}
      </footer>
		);
	}
}

// ES5 的写法
Footer.propTypes = {
	todos: PropTypes.any.isRequired,
	onDeleteCompletedTodos: PropTypes.func.isRequired
};

Footer.defaultProps = {
	todos: [],
	onDeleteCompletedTodos: () => null
};
