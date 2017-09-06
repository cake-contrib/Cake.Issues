---
Order: 20
Description: Compatible versions.
---
The core addins and all issue providers and pull request systems need to be build against a compatible core API.
You can check the required dependencies in the release notes of the addin.

This means that it's not possible to use a version of an issue provider build against `Cake.Issues` 1.0.0
together with a version of a pull request system build against `Cake.Issues` 2.0.0.

We use strict [semantic versioning].
Therefore it's possible to use issue provider and pull request system built against `Cake.Issues` 1.0.0
together with `Cake.Issues` 1.1.0.

:::{.alert .alert-info}
Please note that versions below 1.0.0 are not considered stable and their API will break regularly.
Expect breaking changes in each minor version.

Therefore we strongly advise to pin to specific versions of the addins to avoid breaking builds
once we update the addin.
:::


[semantic versioning]: http://semver.org/