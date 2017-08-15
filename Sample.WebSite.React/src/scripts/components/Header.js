import React from 'react';

import NewTodo from './NewTodo';

export default class Header extends React.Component {
	constructor (props) {
		super(props);
	}

	render () {
		return (
            <header id='header'>
                <h1>todos</h1>
                <NewTodo {...this.props} />
            </header>
		);
	}
}

