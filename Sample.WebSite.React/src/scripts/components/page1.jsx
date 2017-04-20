import React from 'react';

import '../../styles/page1.less';

class Page1 extends React.Component {

  constructor(props) {
    super(props);
    this.state = { likesCount: 0 };
    this.onLike = this.onLike.bind(this);
  }

  onLike() {
    const newLikesCount = this.state.likesCount + 1;
    this.setState({ likesCount: newLikesCount });
  }

  render() {
    return (
      <div className="page1">
        <div>
          <p>This part is from src/scripts/components/page1.jsx</p>
        </div>
        <span>Likes : {this.state.likesCount}</span>
        <div><button onClick={this.onLike}>Like Me</button></div>
      </div>
    );
  }

}

// 加了default后, 在以后import的时候就可以不用花括号了
// 如果不加, import必须用花括号
// export function Func () { }
// import { Func } from 'func'
// export default function Func () { }
// import Func from 'func'
export default Page1;
