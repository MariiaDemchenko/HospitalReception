import React, { Component } from 'react';
import { FaEdit, FaEye, FaPlus, FaSave, FaTimes } from 'react-icons/fa';

class Menu extends Component {

    constructor(props) {
        super(props);
        this.onEditCommand = this.onEditCommand.bind(this)
        this.onViewCommand = this.onViewCommand.bind(this)
        this.onAddCommand = this.onAddCommand.bind(this)
        this.onSaveCommand = this.onSaveCommand.bind(this)
        this.onRemoveCommand = this.onRemoveCommand.bind(this)
    }

    onEditCommand() {
        if (this.props.onEditCommand)
            this.props.onEditCommand.call(this);
    }

    onViewCommand() {
        if (this.props.onViewCommand)
            this.props.onViewCommand.call(this);
    }

    onAddCommand() {
        if (this.props.onAddCommand)
            this.props.onAddCommand.call(this);
    }

    onSaveCommand() {
        if (this.props.onSaveCommand)
            this.props.onSaveCommand.call(this);
    }

    onRemoveCommand() {
        if (this.props.onRemoveCommand)
            this.props.onRemoveCommand.call(this);
    }


    renderButton(command, text, className, include, icon) {
        if (include)
            return (<a className={className}>{text}<i onClick={command}>{icon}</i></a>);
    }

    render() {
        return (
            <div className="menu">
                {this.renderButton(this.onViewCommand, `View `, `btn btn-outline-primary menu-btn`, this.props.includeView, <FaEye/>)}
                {this.renderButton(this.onEditCommand, `Edit `, `btn btn-outline-warning menu-btn`, this.props.includeEdit, <FaEdit />)}
                {this.renderButton(this.onAddCommand, `Add `, `btn btn-outline-success menu-btn`, this.props.includeAdd, <FaPlus />)}
                {this.renderButton(this.onSaveCommand, `Save Changes `, `btn btn-outline-success menu-btn`, this.props.includeSaveChanges, <FaSave />)}
                {this.renderButton(this.onRemoveCommand, `Remove `, `btn btn-outline-danger menu-btn`, this.props.includeRemove, <FaTimes />)}
            </div>
        );
    }
}

Menu.defaultProps = {
    includeSaveChanges: false,
    includeView: true,
    includeEdit: true,
    includeAdd: true,
    includeRemove: true    
};

export default Menu