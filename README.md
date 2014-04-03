GreatGeorge
===========

1GAM April 2014

==========
http://docs.unity3d.com/Documentation/Manual/ExternalVersionControlSystemSupport.html
- When checking the project into a version control system, you should add the Assets and the ProjectSettings directories to the system. The Library directory should be completely ignored - when using external version control, it's only a local cache of imported assets. When creating new assets, make sure both the asset itself and the associated .meta file is added to version control.