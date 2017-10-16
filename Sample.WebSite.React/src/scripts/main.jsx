import React from 'react';
import { render } from 'react-dom';

// 导入这个文件相关的less文件
import '../styles/main.less';

import TodoApp from '../scripts/components/TodoApp.react';

// This should be the preferred option for all stateless components
// 根据React要求, stateless component最好使用pure function
function Main () {
	return (
    <div className='main'>
      <TodoApp />
    </div>
	);
}

render(<Main />, document.getElementById('root'));
