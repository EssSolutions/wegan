import React, {Component} from 'react';

class PageTitle extends Component {
	render() {
		return (
			<div className="app-page-title">
				<div className="page-title-wrapper">
					<div className="page-title-heading">
						<div className="page-title-icon">
							<i className="pe-7s-menu icon-gradient bg-mean-fruit">
							</i>
						</div>
						<div>
							{this.props.title}
						</div>
					</div>
				</div>
			</div>
		);
	}
}

export default PageTitle;