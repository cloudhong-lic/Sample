'use strict';

let counter = 1;

/**
 * This is a simple counter for providing unique ids.
 */
const Counter = {
	increment () {
		return `id-${String(counter++)}`;
	}
};

export default Counter;
