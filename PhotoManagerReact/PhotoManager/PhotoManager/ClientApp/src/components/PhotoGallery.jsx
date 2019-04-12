import React, { Component } from 'react';
import Gallery from 'react-grid-gallery';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';
import { bindActionCreators } from 'redux';
import { actionCreators } from '../store/PhotoGallery';
import Menu from './Menu';

class PhotoGallery extends Component {
    componentWillUnmount() {
        this.props.onBeforeRedirect();
    }

    constructor() {
        super();
        window.onscroll = () => {
            if (window.innerHeight + document.documentElement.scrollTop === document.documentElement.offsetHeight) {
                if (this.props.hasMore) {
                    (this.props.requestAllPhotos(this.props.pageIndex));
                }
            }
        }
    }

    componentDidMount() {
        this.props.requestAllPhotos(this.props.pageIndex);
    }

    render() {
        if (this.props.redirectToView) {
            return (<Redirect push to={{ pathname: `/photoView/${this.props.photoId}`, state: { selectedPhotos: this.props.photos.filter(photo => photo.isSelected) } }} />)
        }
        if (this.props.redirectToEdit) {
            return (<Redirect push to={{ pathname: `/photoEdit/${this.props.photoId}`, state: { selectedPhotos: this.props.photos.filter(photo => photo.isSelected) } }} />)
        }
        return (
            <div>
                <h1>Photo Gallery</h1>
                <Menu onEditCommand={this.props.onPhotoEditClick} onViewCommand={this.props.onPhotoViewClick} />
                {renderAllPhotosGallery(this.props)}
            </div>
        );
    }
}

function renderAllPhotosGallery(props) {
    return (
        <div>
            <Gallery images={props.photos.map(photo => ({
                src: photo.photoUrl,
                thumbnail: photo.photoUrl,
                isSelected: photo.isSelected,
                caption: `${photo.photoName} ShotDate: ${new Date(photo.shotDate).toLocaleString()} Camera Model: ${photo.cameraModel}`,
                thumbnailWidth: 300,
                thumbnailHeight: 170,
                tags: [{ value: photo.photoName, title: photo.photoName }]
            }))}
                onClickImage={props.onClickImage}
                margin={30}
                rowHeight={250}
                onSelectImage={props.onSelectImage}
                onClickThumbnail={props.onClickThumbnail} />
        </div>
    );
}

export default connect(
    state => state.photoGallery,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(PhotoGallery);