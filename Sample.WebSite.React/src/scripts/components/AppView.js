'use strict';

import React from 'react';

import Footer from './Footer';
import Header from './Header';
import Main from './Main';

function AppView (props) {
	return (
    <div>
      <Header {...props} />
      <Main {...props} />
      <Footer {...props} />
    </div>
	);
}

export default AppView;
