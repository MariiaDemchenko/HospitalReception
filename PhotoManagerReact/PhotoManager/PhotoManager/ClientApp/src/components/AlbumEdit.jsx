import React, { Component } from 'react';
import Gallery from 'react-grid-gallery';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';
import { bindActionCreators } from 'redux';
import { actionCreators } from '../store/AlbumPhotos';
import axios from './AxiosInstance';
import Menu from './Menu';
import NotifierGenerator from './Notification';

class AlbumEdit extends Component {

    constructor(props) {
        super(props)
        this.onScroll = this.onScroll.bind(this);
        this.props.onInitAlbum(this.props.location.state.selectedAlbum);
    }

    onScroll() {
        if (window.innerHeight + document.documentElement.scrollTop === document.documentElement.offsetHeight) {
            if (this.props.hasMore) {
                (this.props.requestAllPhotos(this.props.pageIndex, this.props.match.params.albumId));
            }
        }
    }

    componentWillUnmount() {
        window.removeEventListener('scroll', this.onScroll, false);
        this.props.onBeforeRedirect();
    }

    componentDidMount() {
        window.addEventListener('scroll', this.onScroll, false);
        this.props.requestAllPhotos(this.props.pageIndex, this.props.match.params.albumId);
    }

    render() {
        var selectedAlbum = this.props.selectedAlbum || {};
        if (this.props.redirectToAlbums) {
            return (<Redirect push to={{ pathname: `/albums` }} />)
        }
        if (this.props.postSaveChanges) {
            const onRequestSuccess = this.props.onRequestSuccess
            const onRequestError = this.props.onRequestError
            if (this.props.match.params.albumId === "0") {
                axios.post(`api/Albums/AddAlbum`, this.props.selectedAlbum)
                    .then(function (response) {
                        if (response.status === 200) {
                            onRequestSuccess()
                        }
                    })
                    .catch(function (error) {
                        onRequestError(error.response.data)
                    });
            }
            else {
                axios.put(`api/Albums/EditAlbum`, this.props.selectedAlbum)
                    .then(function (response) {
                        if (response.status === 200) {
                            onRequestSuccess()
                        }
                    })
                    .catch(function (error) {
                        onRequestError(error.response.data)
                    });
            }
        }
        return (
            <div>
                <NotifierGenerator messageText={this.props.alertMessage}
                    messageId={this.props.alertId}
                    messageType={this.props.alertType} />

                <h2 className="title">{this.props.match.params.albumId === "0" ? "Add album" : "Edit album"} <span className="badge badge-primary">{this.props.location.state.selectedAlbum.albumName}</span></h2>
                <Menu
                    onEditCommand={this.props.onPhotoEditClick}
                    onViewCommand={this.props.onPhotoViewClick}
                    onSaveCommand={this.props.onSaveChangesClick}
                    includeSaveChanges={true}
                    includeView={false}
                    includeEdit={false}
                    includeAdd={false}
                    includeRemove={false}
                />
                <div>
                    <div className="row row info-fields">
                        <label className="col-md-4">Name</label>
                        <input
                            className={`form-control col-md-4 ${this.props.formErrors.name.length === 0 ? '' : 'has-error'}`}
                            value={selectedAlbum.albumName}
                            onChange={this.props.onAlbumNameChange}
                            onBlur={this.props.nameOnBlur}
                        />
                        <span className="badge badge-danger">{this.props.formErrors.name}</span>
                    </div>

                    <div className="row row info-fields">
                        <label className="col-md-4">Description</label>
                        <input value={selectedAlbum.description}
                            className={`form-control col-md-4 ${this.props.formErrors.description.length === 0 ? '' : 'has-error'}`}
                            onChange={this.props.onAlbumDescriptionChange}
                            onBlur={this.props.descriptionOnBlur}
                        />
                        <span className="badge badge-danger">{this.props.formErrors.description}</span>
                    </div>

                </div>
                {renderAllPhotosGallery(this.props)}
            </div>
        );
    }
}

function renderAllPhotosGallery(props) {
    const albumPhotos = props.albumPhotos || []
    return (
        <Gallery images={albumPhotos.map(photo => ({
            src: `${photo.photoUrl}?v=${props.currentDate}`,
            thumbnail: `${photo.thumbUrl}?v=${props.currentDate}`,
            isSelected: photo.isSelected,
            caption: photo.photoName,
            thumbnailWidth: 300,
            thumbnailHeight: 170,
            id: photo.photoId,
            tags: [{ value: photo.photoName, title: photo.photoName }]
        }))}
            onClickImage={props.onClickImage}
            margin={30}
            rowHeight={250}
            onSelectImage={props.onSelectImage}
            onClickThumbnail={props.onClickThumbnail} />
    );
}

export default connect(
    state => state.albumPhotos,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(AlbumEdit);