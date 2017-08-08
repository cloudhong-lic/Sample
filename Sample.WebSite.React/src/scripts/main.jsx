import React from 'react';
import { render } from 'react-dom';

// 导入这个文件相关的less文件
import '../styles/main.less';

import Page1 from '../scripts/components/page1';
import AppContainer from '../scripts/containers/AppContainer';

// This should be the preferred option for all stateless components
// 根据React要求, stateless component最好使用pure function
function Main() {
  return (
    <div className="main">
      <p className="my-div">
        This part is from src/scripts/index.jsx. 使用了全局的颜色: 粉色
      </p>

      {/* 在此插入子页面 Page1*/}
      {/* <Page1 /> */}
      <AppContainer />
    </div>
  );
}

render(<Main />, document.getElementById('main'));
