import React from 'react';
import {render} from 'react-dom';

// 导入这个文件相关的less文件
// TODO: 并不确定这是正确的做法
import '../styles/app.less'

class App extends React.Component {
    render () {
        return (
            <div>
                <div className="my-div">
                    this is div
                </div>
                <div>
                    <p> Hello React!</p>
                </div>
            </div>
        );
    }
}

render(<App/>, document.getElementById('app'));