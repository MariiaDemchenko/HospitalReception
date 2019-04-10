import axios from './AxiosInstance';
import React, { Component } from 'react';
import Gallery from 'react-grid-gallery';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';
import { bindActionCreators } from 'redux';
import { actionCreators } from '../store/AlbumPhotos';
import Menu from './Menu';
import NotifierGenerator from './Notification';

class AlbumPhotos extends Component {

    constructor(props) {
        super(props);
        this.onScroll = this.onScroll.bind(this);
        if (this.props.location.state && this.props.location.state.selectedAlbum) {
            this.props.onInitAlbum(this.props.location.state.selectedAlbum)
        }
        else {
            this.props.onInitAlbum({})
            this.props.requestAlbumPhotos(this.props.pageIndex, this.props.match.params.albumId);
        }
    }

    componentWillUnmount() {
        window.removeEventListener('scroll', this.onScroll, false);
        this.props.onBeforeRedirect();
    }

    onScroll() {
        if (window.innerHeight + document.documentElement.scrollTop === document.documentElement.offsetHeight) {
            if (this.props.hasMore) {
                (this.props.requestAlbumPhotos(this.props.pageIndex, this.props.match.params.albumId));
            }
        }
    }

    componentDidMount() {
        window.addEventListener('scroll', this.onScroll, false);
        this.props.requestAlbumPhotos(0, this.props.match.params.albumId);
    }

    render() {
        const selectedAlbum = this.props.selectedAlbum || {};

        if (this.props.refresh) {
            this.props.requestAlbumPhotos(this.props.pageIndex, this.props.match.params.albumId);
        }

        if (this.props.redirectToRemove) {
            const selectedPhotos = this.props.albumPhotos.filter(photo => photo.isSelected === true);
            const photos = selectedPhotos.map(photo => ({ photoId: photo.photoId, albumId: this.props.match.params.albumId }))
            const onRemoveSuccess = this.props.onRemoveSuccess
            axios.post(`api/Photos/RemovePhotos`, photos)
                .then(function (response) {
                    if (response.status === 200) {
                        onRemoveSuccess()
                    }
                })
                .catch(function (error) {
                    if (error.response && error.response.data) {
                        this.props.onRemoveError(error.response.data)
                    }
                });
        }
        if (this.props.redirect) {
            return (<Redirect push to={{ pathname: `/photoAdd/${this.props.match.params.albumId}`, state: { selectedAlbum: this.props.selectedAlbum } }} />)
        }
        if (this.props.redirectToView) {
            return (<Redirect push to={{ pathname: `/photoView/${this.props.match.params.albumId}`, state: { selectedPhotos: this.props.albumPhotos.filter(photo => photo.isSelected), selectedAlbum: this.props.selectedAlbum } }} />)
        }
        if (this.props.redirectToEdit) {
            return (<Redirect push to={{ pathname: `/photoEdit/${this.props.match.params.albumId}`, state: { selectedPhotos: this.props.albumPhotos.filter(photo => photo.isSelected), selectedAlbum: this.props.selectedAlbum } }} />)
        }
        if (!this.props.albumPhotos || this.props.albumPhotos.length === 0) {
            return <h2>No photos in this album yet</h2>
        }
        return (
            <div>
                <NotifierGenerator messageText={this.props.alertMessage}
                    messageId={this.props.alertId}
                    messageType={this.props.alertType} />
                <h2 className="title">Enjoy photos is album <span className="badge badge-primary">{selectedAlbum.albumName}</span></h2>
                <h3 className="title"><span className="badge badge-warning">{selectedAlbum.description}</span></h3>
                {renderMenu(this.props)}
                {renderPhotoGallery(this.props)}
            </div>
        );
    }
}

function renderPhotoGallery(props) {
    const albumPhotos = props.albumPhotos || []
    return (
        <div>
            <Gallery images={albumPhotos.map(photo => ({
                src: `${photo.photoUrl}?v=${props.currentDate}`,
                thumbnail: `${photo.thumbUrl}?v=${props.currentDate}`,
                isSelected: photo.isSelected,
                caption: photo.photoName,
                thumbnailWidth: 300,
                thumbnailHeight: 170,
                tags: [{ value: photo.photoName, title: photo.photoName }]
            }))}
                margin={30}
                rowHeight={250}
                onSelectImage={props.onSelectImage}
            />
        </div>
    );
}

function renderMenu(props) {
    if (localStorage.getItem("token")) {
        return (
            <Menu
                onEditCommand={props.onPhotoEditClick}
                onViewCommand={props.onPhotoViewClick}
                onAddCommand={props.onPhotoAddClick}
                onRemoveCommand={props.onPhotoRemoveClick}
            />
        );
    }
}

export default connect(
    state => state.albumPhotos,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(AlbumPhotos);

