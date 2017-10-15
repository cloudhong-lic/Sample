﻿import React from 'react';
import { render } from 'react-dom';

// 导入这个文件相关的less文件
import '../styles/main.less';

import Page1 from '../scripts/components/page1';
import TodoApp from '../scripts/components/TodoApp.react';

// This should be the preferred option for all stateless components
// 根据React要求, stateless component最好使用pure function
function Main () {
	return (
    <div className='main'>
      {/* 在此插入子页面 Page1*/}
      {/* <Page1 /> */}
      <TodoApp />
    </div>
	);
}

render(<Main />, document.getElementById('root'));
