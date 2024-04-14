---
Title: FilePathTooLong
Description: The path of a file is too long.
---

<?# Table Class=table HeaderRows=0 ?>
<?*
"Rule Id" FilePathTooLong
Priority Warning
"Available in" "0.7.3 or higher"
?>
<?#/ Table ?>

## Cause

The path of a file in the repository is too long.

## Rule description

Some operating systems and applications have a limitation of maximum path length which they can handle.
To guarantee proper building this length should not be exceeded.

## How to fix violations

Rename the name of the file or shorten the path name.
